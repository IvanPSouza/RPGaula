using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorInimigo : MonoBehaviour
{
    public enum EstadoInimigo { Parado, Patrulha, Perseguicao }

    [Header("AI do inimigo")]
    public EstadoInimigo estadoAtual;

    [Header("Movimentação")]
    public float velocidade;
    public Transform[] pontosDePatrulha;
    public int indicePontoAtual = 0;

    [Header("Espera")]
    public float tempoDeEspera = 2.0f;
    public float cronometroEspera = 0f;

    [Header("Sensores")]
    public float raioVisao = 5f;
    public float raioPerseguicao = 8f;
    public float distanciaAtaque = 1f;

    public string meuNomeID; // Configure isso no Inspector (ex: "Mole")

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject jogador;

    //NOVAS REFERÊNCIAS DE FÍSICA
    private Rigidbody2D rb;
    private Collider2D meuCollider;

    // Ponte Update -> FixedUpdate
    private Vector2 destinoMovimento;
    private float velocidadeAtual;
    private bool estaSeMovendo = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        meuCollider = GetComponent<Collider2D>();

        estadoAtual = EstadoInimigo.Patrulha;

        if (jogador == null)
        {
            jogador = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (jogador == null) return;

        estaSeMovendo = false;

        float distancia = Vector2.Distance(transform.position, jogador.transform.position);

        switch (estadoAtual)
        {
            case EstadoInimigo.Parado:
                animator.SetBool("Andando", false);
                break;

            case EstadoInimigo.Patrulha:
                animator.speed = 1f;

                if (distancia < raioVisao)
                    estadoAtual = EstadoInimigo.Perseguicao;

                Patrulhar();
                break;

            case EstadoInimigo.Perseguicao:
                animator.speed = 1.2f;

                if (distancia > raioPerseguicao)
                    estadoAtual = EstadoInimigo.Patrulha;

                if (distancia < distanciaAtaque)
                    IniciarCombate();
                else
                    Perseguir();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (estaSeMovendo)
        {
            Vector2 novaPosicao = Vector2.MoveTowards(
                rb.position,
                destinoMovimento,
                velocidadeAtual * Time.fixedDeltaTime
            );

            rb.MovePosition(novaPosicao);
        }
    }

    void IniciarCombate()
    {
        // 1. Anota quem é o inimigo
        DadosGlobais.inimigoParaGerar = meuNomeID;

        // 2. Carrega a cena
        SceneManager.LoadScene("CenaBatalha");
    }

    private void Patrulhar()
    {
        if (meuCollider != null)
            meuCollider.isTrigger = true;

        Transform alvo = pontosDePatrulha[indicePontoAtual];

        if (Vector2.Distance(transform.position, alvo.position) < 0.1f)
        {
            animator.SetBool("Andando", false);

            cronometroEspera += Time.deltaTime;

            if (cronometroEspera >= tempoDeEspera)
            {
                cronometroEspera = 0;
                indicePontoAtual++;

                if (indicePontoAtual >= pontosDePatrulha.Length)
                    indicePontoAtual = 0;
            }
        }
        else
        {
            MoverFisico(alvo.position, velocidade);
        }
    }

    private void Perseguir()
    {
        if (meuCollider != null)
            meuCollider.isTrigger = false;

        MoverFisico(jogador.transform.position, velocidade * 1.5f);
    }

    private void MoverFisico(Vector3 destino, float velocidadeMovimento)
    {
        Vector3 direcao = (destino - transform.position).normalized;

        animator.SetBool("Andando", true);
        animator.SetFloat("Horizontal", direcao.x);
        animator.SetFloat("Vertical", direcao.y);

        if (direcao.x < -0.1f)
            spriteRenderer.flipX = true;
        else if (direcao.x > 0.1f)
            spriteRenderer.flipX = false;

        destinoMovimento = destino;
        velocidadeAtual = velocidadeMovimento;
        estaSeMovendo = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioVisao);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioPerseguicao);

        Gizmos.color = Color.blue;
        if (jogador != null)
            Gizmos.DrawLine(transform.position, jogador.transform.position);
    }
}
