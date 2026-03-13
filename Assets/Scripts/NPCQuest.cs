using UnityEngine;
using TMPro;

public class NPCQuest : MonoBehaviour
{
    [Header("Identidade")]
    public string nome;

    [Header("Apenas para o primeiro NPC do Jogo")]
    public Quest questInicial;

    [Header("Visuais")]
    public SpriteRenderer indicadorVisual; //AZUL [inicio] | LARANJA [destino]

    [Header("Interface de Diálogo (NOVO)")]
    public GameObject painelDialogo;
    public TextMeshProUGUI textoDialogo;

    private bool jogadorPerto = false;
    private GameObject jogadorRef;

    private void Start()
    {
        if (questInicial != null && DadosGlobais.questDisponivel == null && DadosGlobais.QuestAtiva == null && DadosGlobais.historiaConcluida == false)
        {
            DadosGlobais.questDisponivel = questInicial;
        }
    }

    private void Update()
    {
        AtualizarIconeVisual();
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            Interagir();
        }
    }

    private void AtualizarIconeVisual()
    {
        if (indicadorVisual == null) return;

        indicadorVisual.gameObject.SetActive(false);

        if (DadosGlobais.historiaConcluida) return;

        //AZU (nova quest)
        if (DadosGlobais.QuestAtiva == null && DadosGlobais.questDisponivel != null && DadosGlobais.questDisponivel.nomeNPCEmissor == nome)
        {
            indicadorVisual.color = Color.blue;
            indicadorVisual.gameObject.SetActive(true);
        }
        else if (DadosGlobais.QuestAtiva != null && DadosGlobais.QuestAtiva.nomeNPCDestino == nome)//LARANJA (entrega)
        {
            indicadorVisual.color = Color.orange;
            indicadorVisual.gameObject.SetActive(true);
        }
    }

    public void Interagir()
    {
        // 1. Liga o painel visual
        if (painelDialogo != null) painelDialogo.SetActive(true);
        if (textoDialogo == null) return;

        // 2. Os antigos Debug.Logs viram textoDialogo.text!
        if (DadosGlobais.historiaConcluida)
        {
            textoDialogo.text = "Parabens por chegar na superficie cenourinha!";
            return;
        }

        //Cenario 1: Temos uma missão
        if (DadosGlobais.QuestAtiva != null)
        {
            Quest quest = DadosGlobais.QuestAtiva;

            if (quest.nomeNPCDestino == nome)
            {
                bool terminouCaca = (quest.tipoMissao == TipoQuest.CacarMonstros && DadosGlobais.progressoAtual >= quest.quantidade);

                bool terminouColeta = (quest.tipoMissao == TipoQuest.ColetarItens && DadosGlobais.progressoAtual >= quest.quantidade);

                bool terminouEntrega = (quest.tipoMissao == TipoQuest.EncontrarNPC);

                bool terminouChegar = (quest.tipoMissao == TipoQuest.ChegueLocal);

                if (terminouCaca || terminouChegar || terminouColeta || terminouEntrega)
                {
                    textoDialogo.text = quest.falaConclusao + "\n\n(Recebeu " + quest.recompensaOuro + " Ouro!)";
                    EntregarRecompensa(quest);
                }
                else
                {
                    // INCLUINDO O NOME DO ITEM AQUI!
                    textoDialogo.text = quest.falaAndamento + "\n(Progresso: " + DadosGlobais.progressoAtual + "/" + quest.quantidade + " " + quest.nomeObjetivo + ")";
                }

            }
            else
            {
                textoDialogo.text = "O " + quest.nomeNPCDestino + " está à sua espera. Não perca tempo aqui!";
            }
            return;
        }

        if (DadosGlobais.questDisponivel != null)
        {
            if (DadosGlobais.questDisponivel.nomeNPCEmissor == nome)
            {
                textoDialogo.text = DadosGlobais.questDisponivel.falaInicio;
                DadosGlobais.QuestAtiva = DadosGlobais.questDisponivel;
                DadosGlobais.questDisponivel = null;
                DadosGlobais.progressoAtual = 0;
            }
            else { textoDialogo.text = "Ouvi dizer que o " + DadosGlobais.questDisponivel.nomeNPCEmissor + " está à sua procura."; }
            return;
        }

        textoDialogo.text = "Olá Cenoura! O dia está lindo hoje.";
    }
    public void FecharDialogo()
    {
        if (painelDialogo != null) painelDialogo.SetActive(false);
    }

    void EntregarRecompensa(Quest questConcluida)
    {
        //Entrega as recompensas ao player
        DadosGlobais.moedasAtualJogador += questConcluida.recompensaOuro;
        DadosGlobais.xpAtualJogador += questConcluida.recompensaXP;

        //Proxima quest da questline
        DadosGlobais.QuestAtiva = null;
        DadosGlobais.questDisponivel = questConcluida.proximaQuest;

        //se não tem quests disponiveis a historia foi concluida!
        if (DadosGlobais.questDisponivel == null)
        {
            DadosGlobais.historiaConcluida = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = true;
            jogadorRef = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = false; 
            FecharDialogo();
        }
    }
}
