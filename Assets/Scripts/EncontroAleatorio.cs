using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncontroAleatorio : MonoBehaviour
{
    [Header("Identificação")]
    public string idUnico;

    [Header("Formação Inimiga")]
    public List<GameObject> inimigos;

    [Header("Chance de encontro")]
    public int chanceMax = 100;

    // ===== CONTROLE DE TEMPO =====
    private float proximoCheck = 0f;
    public float intervalo = 1f;

    // ===== CONTROLE DE MOVIMENTO =====
    private Vector2 ultimaPosicao;
    private bool primeiraVez = true;

    // ===== NOVO: DELAY AO ENTRAR =====
    private float tempoLiberado = 0f;
    public float delayInicial = 5f;

    // ===== Darkness ======
    public GameObject Darkness1;
    public GameObject Darkness2;
    public GameObject Darkness3;

    [Header("Delay entre Darkness")]
    public float delayEntreDarkness = 0.5f;

    // ===== CONTROLE DE COROUTINE =====
    private Coroutine rotinaAtual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tempoLiberado = Time.time + delayInicial;

            if (rotinaAtual != null)
                StopCoroutine(rotinaAtual);

            rotinaAtual = StartCoroutine(AtivarDarknessSequencial());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (rotinaAtual != null)
                StopCoroutine(rotinaAtual);

            rotinaAtual = StartCoroutine(DesativarDarknessSequencial());
        }
    }

    IEnumerator AtivarDarknessSequencial()
    {
        Darkness1.SetActive(true);
        yield return new WaitForSeconds(delayEntreDarkness);

        Darkness2.SetActive(true);
        yield return new WaitForSeconds(delayEntreDarkness);

        Darkness3.SetActive(true);
    }

    IEnumerator DesativarDarknessSequencial()
    {
        Darkness3.SetActive(false);
        yield return new WaitForSeconds(delayEntreDarkness);

        Darkness2.SetActive(false);
        yield return new WaitForSeconds(delayEntreDarkness);

        Darkness1.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // BLOQUEIO DE 5 SEGUNDOS
            if (Time.time < tempoLiberado)
            {
                Debug.Log($"Ainda não. Faltam {(tempoLiberado - Time.time):F2}s");
                return;
            }

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb == null) return;

            // ===== CALCULA VELOCIDADE REAL =====
            if (primeiraVez)
            {
                ultimaPosicao = rb.position;
                primeiraVez = false;
                return;
            }

            float distancia = Vector2.Distance(rb.position, ultimaPosicao);
            float velocidadeCalculada = distancia / Time.deltaTime;

            ultimaPosicao = rb.position;

            bool estaMovendo = velocidadeCalculada > 0.1f;

            if (!estaMovendo)
                return;

            // ===== CONTROLE DE TEMPO =====
            if (Time.time < proximoCheck)
                return;

            proximoCheck = Time.time + intervalo;

            int numero = Random.Range(1, chanceMax + 1);
            IniciadorBatalha iniciador = GetComponent<IniciadorBatalha>();

            if (numero == 1)
            {
                Debug.Log("foi dessa vez");

                if (iniciador != null)
                {
                    List<int> niveisExtraidos = new List<int>();
                    AtributosCombate[] inimigosCena = GetComponentsInChildren<AtributosCombate>();

                    foreach (AtributosCombate inimigo in inimigosCena)
                    {
                        niveisExtraidos.Add(inimigo.nivel);
                    }

                    iniciador.DispararBatalha(collision.gameObject, idUnico, inimigos, niveisExtraidos);
                }
            }
            else
            {
                Debug.Log("Não foi dessa vez");
            }
        }
    }
}