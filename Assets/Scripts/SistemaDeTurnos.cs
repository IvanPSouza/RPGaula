using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum EstadoBatalha
{
    Preparacao, TurnoJogador, TurnoInimigo, Vitoria, Derrota
}

public class SistemaDeTurnos : MonoBehaviour
{
    public EstadoBatalha estadoAtual;

    [Header("UI")]
    public Slider slideHeroi;
    public Button btnPocao;
    public Button btnFlecha;

    [Header("Referências")]
    public SistemaInventario inventario;
    public DadosItem pocaoDeVida;
    public DadosItem flecha;

    [Header("Texto de feedback")]
    public TextMeshProUGUI textoFeedback;

    private AtributosCombate atributosHeroi;
    private List<AtributosCombate> inimigosVivos = new List<AtributosCombate>();

    // ===== NOVAS VARIÁVEIS =====
    private int xpGanhoTotal = 0;
    private int ouroGanhoTotal = 0;
    private bool subiuNivel = false;
    private int nivelAntes;

    private void Start()
    {
        estadoAtual = EstadoBatalha.Preparacao;
        StartCoroutine(ConfigurarBatalha());
    }

    IEnumerator ConfigurarBatalha()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player com tag 'Player' não encontrado!");
            yield break;
        }

        atributosHeroi = player.GetComponent<AtributosCombate>();

        if (atributosHeroi == null)
        {
            Debug.LogError("AtributosCombate não encontrado no Player!");
            yield break;
        }

        atributosHeroi.minhaBarraDeVida = slideHeroi;
        atributosHeroi.AtualizarBarra();

        // ===== GUARDA NÍVEL INICIAL =====
        nivelAntes = atributosHeroi.nivel;

        if (inventario != null && pocaoDeVida != null)
        {
            if (!inventario.TemItem(pocaoDeVida, 1))
                if (btnPocao != null)
                    btnPocao.interactable = false;
        }

        if (inventario != null && flecha != null)
        {
            if (!inventario.TemItem(flecha, 1))
                if (btnFlecha != null)
                    btnFlecha.interactable = false;
        }

        yield return new WaitForSeconds(1f);

        GameObject[] objsInimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        foreach (GameObject obj in objsInimigos)
        {
            AtributosCombate inimigo = obj.GetComponent<AtributosCombate>();

            if (inimigo != null)
                inimigosVivos.Add(inimigo);
        }

        IniciarTurnoJogador();
    }

    void IniciarTurnoJogador()
    {
        textoFeedback.text = "Turno da Cenoura, escolha uma ação";
        estadoAtual = EstadoBatalha.TurnoJogador;
    }

    public void BotaoAtacarFraco()
    {
        if (estadoAtual != EstadoBatalha.TurnoJogador) return;
        if (inimigosVivos.Count == 0) return;

        AtributosCombate alvo = inimigosVivos[0];
        alvo.ReceberDano(atributosHeroi.danoAtual);

        textoFeedback.text = $"Você causou {atributosHeroi.danoAtual} de dano";

        if (alvo.hpAtual <= 0)
        {
            RecompensaInimigo loot = alvo.GetComponent<RecompensaInimigo>();
            ProgressoJogador progresso = atributosHeroi.GetComponent<ProgressoJogador>();

            if (loot != null && progresso != null)
            {
                progresso.GanharXP(loot.xpDrop);
                DadosGlobais.moedasAtualJogador += loot.moedasDrop;

                // ===== ACUMULA =====
                xpGanhoTotal += loot.xpDrop;
                ouroGanhoTotal += loot.moedasDrop;

                DadosGlobais.xpAtualJogador = progresso.xpAtual;
                DadosGlobais.nivelAtualJogador = atributosHeroi.nivel;

                // ===== VERIFICA LEVEL UP =====
                if (atributosHeroi.nivel > nivelAntes)
                {
                    subiuNivel = true;
                    nivelAntes = atributosHeroi.nivel;
                }
            }

            inimigosVivos.RemoveAt(0);
        }

        VerificarFimDeTurnoJogador();
    }

    public void BotaoAtacarForte()
    {
        if (estadoAtual != EstadoBatalha.TurnoJogador) return;
        if (inimigosVivos.Count == 0) return;

        bool consumiuFlecha = false;

        for (int i = 0; i < DadosGlobais.inventarioAtual.Count; i++)
        {
            SlotInventario slot = DadosGlobais.inventarioAtual[i];

            if (slot.dadosDoItem == flecha && slot.quantidade > 0)
            {
                slot.quantidade--;
                consumiuFlecha = true;

                if (slot.quantidade <= 0)
                {
                    DadosGlobais.inventarioAtual.RemoveAt(i);
                    if (btnFlecha != null)
                        btnFlecha.interactable = false;
                }

                break;
            }
        }

        if (!consumiuFlecha)
        {
            textoFeedback.text = "Você não tem flechas para este ataque!";
            return;
        }
        else
        {
            textoFeedback.text = $"Você causou {atributosHeroi.danoAtual * 2} de dano com uma flecha";
        }

        AtributosCombate alvo = inimigosVivos[0];
        alvo.ReceberDano(atributosHeroi.danoAtual * 2);

        if (alvo.hpAtual <= 0)
        {
            RecompensaInimigo loot = alvo.GetComponent<RecompensaInimigo>();
            ProgressoJogador progresso = atributosHeroi.GetComponent<ProgressoJogador>();

            if (loot != null && progresso != null)
            {
                progresso.GanharXP(loot.xpDrop);
                DadosGlobais.moedasAtualJogador += loot.moedasDrop;

                // ===== ACUMULA =====
                xpGanhoTotal += loot.xpDrop;
                ouroGanhoTotal += loot.moedasDrop;

                DadosGlobais.xpAtualJogador = progresso.xpAtual;
                DadosGlobais.nivelAtualJogador = atributosHeroi.nivel;

                // ===== VERIFICA LEVEL UP =====
                if (atributosHeroi.nivel > nivelAntes)
                {
                    subiuNivel = true;
                    nivelAntes = atributosHeroi.nivel;
                }
            }

            inimigosVivos.RemoveAt(0);
        }

        VerificarFimDeTurnoJogador();
    }

    public void BotaoPocao()
    {
        if (estadoAtual != EstadoBatalha.TurnoJogador)
            return;

        bool consumiu = false;

        foreach (SlotInventario slot in DadosGlobais.inventarioAtual)
        {
            if (slot.dadosDoItem == pocaoDeVida && slot.quantidade > 0)
            {
                slot.quantidade--;
                consumiu = true;

                if (slot.quantidade <= 0)
                {
                    DadosGlobais.inventarioAtual.Remove(slot);

                    if (btnPocao != null)
                        btnPocao.interactable = false;
                }

                break;
            }
        }

        if (consumiu)
        {
            atributosHeroi.ReceberCura(50);
            textoFeedback.text = "Você bebeu a poção! Recuperou 50 de vida";
            VerificarFimDeTurnoJogador();
        }
        else
        {
            textoFeedback.text = "Você não tem mais poções!";
        }
    }

    void VerificarFimDeTurnoJogador()
    {
        if (inimigosVivos.Count <= 0)
        {
            estadoAtual = EstadoBatalha.Vitoria;
            StartCoroutine(FinalizarBatalha(true));
        }
        else
        {
            estadoAtual = EstadoBatalha.TurnoInimigo;
            StartCoroutine(TurnoDoInimigo());
        }
    }

    IEnumerator TurnoDoInimigo()
    {
        int indice = 1;

        foreach (AtributosCombate inimigo in inimigosVivos)
        {
            yield return new WaitForSeconds(2f);

            atributosHeroi.ReceberDano(inimigo.danoAtual);

            if (inimigosVivos.Count > 1)
                textoFeedback.text = $"Praga {indice} atacou causando {inimigo.danoAtual} de dano";
            else
                textoFeedback.text = $"Praga atacou causando {inimigo.danoAtual} de dano";

            indice++;

            if (atributosHeroi.hpAtual <= 0)
                break;
        }

        yield return new WaitForSeconds(2f);

        if (atributosHeroi.hpAtual <= 0)
        {
            estadoAtual = EstadoBatalha.Derrota;
            StartCoroutine(FinalizarBatalha(false));
        }
        else
        {
            IniciarTurnoJogador();
        }
    }

    IEnumerator FinalizarBatalha(bool jogadorVenceu)
    {
        ProgressoJogador progresso = atributosHeroi.GetComponent<ProgressoJogador>();

        DadosGlobais.hpAtualJogador = atributosHeroi.hpAtual;
        DadosGlobais.nivelAtualJogador = atributosHeroi.nivel;

        if (progresso != null)
            DadosGlobais.xpAtualJogador = progresso.xpAtual;

        yield return new WaitForSeconds(2f);

        if (jogadorVenceu)
        {
            string mensagem = $"Cenoura venceu!\n+{xpGanhoTotal} XP\n+{ouroGanhoTotal} Ouro";

            if (subiuNivel)
                mensagem += "\nLEVEL UP!";

            textoFeedback.text = mensagem;

            yield return new WaitForSeconds(2f);

            DadosGlobais.inimigosDerrotados.Add(DadosGlobais.idInimigoEmCombate);

            SceneManager.LoadScene("Mundo");
        }
        else
        {
            textoFeedback.text = $"Esse é o fim de Cenoura";
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("GameOver");
        }
    }
}