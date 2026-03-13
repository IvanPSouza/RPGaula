
using UnityEngine;
using TMPro;

public class HUDMissao : MonoBehaviour
{
    [Header("HUD da Missão")]
    public TextMeshProUGUI textoTrackerQuest;

    void Update()
    {
        if (textoTrackerQuest == null) return;

        if (DadosGlobais.historiaConcluida)
        {
            textoTrackerQuest.text = "História Concluída!";
        }
        else if (DadosGlobais.QuestAtiva != null)
        {

            if (DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.CacarMonstros || DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.ColetarItens)
                textoTrackerQuest.text = "Missão Ativa: " + DadosGlobais.QuestAtiva.descricaoNoHud + " (" + DadosGlobais.progressoAtual + "/" + DadosGlobais.QuestAtiva.quantidade + " " + DadosGlobais.QuestAtiva.nomeObjetivo + ")";
            else
                textoTrackerQuest.text = "Missão Ativa: " + DadosGlobais.QuestAtiva.descricaoNoHud;
        }
        else if (DadosGlobais.questDisponivel != null)
        {
            textoTrackerQuest.text = "Nova Missão: Procure o triângulo azul no(a) " + DadosGlobais.questDisponivel.nomeNPCEmissor + " Moedas são os placeholders para missões de coleta, não colete nenhuma ate começar a  missão, Aperte 'I' para abrir o inventario e 'C' para o menu de crafting";
        }
        else
        {
            textoTrackerQuest.text = "Nenhuma missão ativa.";
        }
    }
}


