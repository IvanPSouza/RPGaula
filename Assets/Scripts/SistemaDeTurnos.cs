using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Referências")]
    public SistemaInventario inventario; // agora ligado no inspector
    public DadosItem pocaoDeVida;
    public DadosItem flecha;

    public string nomeDoPrefab; // Nome do objeto na cena
    public bool Coelho = false;

    private AtributosCombate atributosHeroi;
    private List<AtributosCombate> inimigosVivos = new List<AtributosCombate>();

    private void Start()
    {
        estadoAtual = EstadoBatalha.Preparacao;
        StartCoroutine(ConfigurarBatalha());

    }
    /*
    private void Update()
    {
        ProcurarNaCena();
    }

    void ProcurarNaCena()
    {
        GameObject obj = GameObject.Find(nomeDoPrefab);

        if (obj != null)
        {
            Coelho = true;
            Debug.Log("Prefab encontrado!");
        }
        else
        {
            Coelho = false;
            Debug.Log("Prefab não encontrado.");
        }
    }
    */

    IEnumerator ConfigurarBatalha()
    {
        // ===== HEROI =====
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

        // ===== INVENTÁRIO =====
        if (inventario != null && pocaoDeVida != null)
        {
            if (!inventario.TemItem(pocaoDeVida, 1))
            {
                if (btnPocao != null)
                    btnPocao.interactable = false;
            }
        }
        else
        {
            Debug.LogWarning("Inventário ou Poção não configurados no Inspector.");
        }

        Debug.Log("Preparando a batalha...");
        yield return new WaitForSeconds(1f);

        // ===== INIMIGOS =====
        GameObject[] objsInimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        foreach (GameObject obj in objsInimigos)
        {
            AtributosCombate inimigo = obj.GetComponent<AtributosCombate>();

            if (inimigo != null)
                inimigosVivos.Add(inimigo);
        }

        if (inimigosVivos.Count == 0)
        {
            Debug.LogWarning("Nenhum inimigo encontrado na cena!");
        }

        IniciarTurnoJogador();
    }

    void IniciarTurnoJogador()
    {
        Debug.Log("Sua vez. Pressione o botão de ataque!");
        estadoAtual = EstadoBatalha.TurnoJogador;
    }

    public void BotaoAtacarFraco()
    {
        if (estadoAtual != EstadoBatalha.TurnoJogador)
            return;

        if (inimigosVivos.Count == 0)
            return;

        AtributosCombate alvo = inimigosVivos[0];
        alvo.ReceberDano(atributosHeroi.danoAtual);

        if (alvo.hpAtual <= 0)
        {
            RecompensaInimigo loot = alvo.GetComponent<RecompensaInimigo>();
            ProgressoJogador progresso = atributosHeroi.GetComponent<ProgressoJogador>();

            if (loot != null && progresso != null)
            {
                progresso.GanharXP(loot.xpDrop);
                DadosGlobais.moedasAtualJogador += loot.moedasDrop;

                Debug.Log($"Você encontrou {loot.moedasDrop} moedas!");

                DadosGlobais.xpAtualJogador = progresso.xpAtual;
                DadosGlobais.nivelAtualJogador = atributosHeroi.nivel;

               /*
               if (DadosGlobais.QuestAtiva != null && Coelho)
                {
                    if (DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.CacarMonstros ||
                        DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.ColetarItens)
                    {
                        DadosGlobais.progressoAtual++;
                        Debug.Log($"Quest: {DadosGlobais.progressoAtual}/{DadosGlobais.QuestAtiva.quantidade}");
                    }
                }
                 //*/
            }

            inimigosVivos.RemoveAt(0);
        }

        VerificarFimDeTurnoJogador();
    }

    public void BotaoAtacarForte()
    {
        if (estadoAtual != EstadoBatalha.TurnoJogador)
            return;

        if (inimigosVivos.Count == 0)
            return;

        // Verifica se há poção disponível
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
                    if (btnPocao != null)
                        btnPocao.interactable = false;
                }

                break;
            }
        }

        if (!consumiuFlecha)
        {
            Debug.Log("Você não tem poções para este ataque!");
            return; // não realiza ataque
        }

        // Escolhe o inimigo alvo (o primeiro da lista)
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

                Debug.Log($"Você encontrou {loot.moedasDrop} moedas!");

                DadosGlobais.xpAtualJogador = progresso.xpAtual;
                DadosGlobais.nivelAtualJogador = atributosHeroi.nivel;
            }

            // Remove o inimigo morto da lista
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
            Debug.Log("Você bebeu a poção!");
            VerificarFimDeTurnoJogador();
        }
        else
        {
            Debug.LogWarning("Você não tem mais poções!");
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
        Debug.Log("Inimigos pensando...");

        foreach (AtributosCombate inimigo in inimigosVivos)
        {
            yield return new WaitForSeconds(2f);

            atributosHeroi.ReceberDano(inimigo.danoBase);

            if (atributosHeroi.hpAtual <= 0)
                break;
        }

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
            Debug.Log("Vitória!");

            DadosGlobais.inimigosDerrotados.Add(DadosGlobais.idInimigoEmCombate);

            SceneManager.LoadScene("Mundo");
        }
        else
        {
            Debug.Log("Derrota...");
            SceneManager.LoadScene("GameOver");
        }
    }
}