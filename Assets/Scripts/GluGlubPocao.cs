using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GluGlubPocao : MonoBehaviour
{
    [Header("Referências")]
    public DadosItem pocaoDeVida;

    [Header("UI")]
    public TextMeshProUGUI textoFeedback;

    private AtributosCombate atributosPlayer;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            atributosPlayer = player.GetComponent<AtributosCombate>();
        }
    }

    public void UsarPocao()
    {
        if (atributosPlayer == null) return;

        if (atributosPlayer.hpAtual >= atributosPlayer.hpMaximo)
        {
            if (textoFeedback != null)
                textoFeedback.text = "Vida cheia!";
            return;
        }

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
                }

                break;
            }
        }

        if (consumiu)
        {
            atributosPlayer.ReceberCura(50);

            // ATUALIZA DADO GLOBAL
            DadosGlobais.hpAtualJogador = atributosPlayer.hpAtual;

            if (textoFeedback != null)
                textoFeedback.text = "Poção usada!";
        }
        else
        {
            if (textoFeedback != null)
                textoFeedback.text = "Sem poções!";
        }
    }
}
