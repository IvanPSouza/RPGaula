using UnityEngine;
using TMPro;

public class GluGlubPocao : MonoBehaviour
{
    [Header("Referências")]
    public DadosItem pocaoDeVida;

    [Header("UI")]
    public TextMeshProUGUI textoFeedback;

    private AtributosCombate atributosPlayer;
    private SistemaInventario inventarioSistema;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            atributosPlayer = player.GetComponent<AtributosCombate>();

        inventarioSistema = FindObjectOfType<SistemaInventario>();
    }

    public void UsarPocao()
    {
        if (atributosPlayer == null || inventarioSistema == null) return;

        if (atributosPlayer.hpAtual >= atributosPlayer.hpMaximo)
        {
            if (textoFeedback != null)
                textoFeedback.text = "Vida cheia!";
            return;
        }

        // Verifica se o jogador tem pelo menos 1 poção
        if (inventarioSistema.TemItem(pocaoDeVida, 1))
        {
            // Remove 1 poção do inventário usando o método do SistemaInventario
            inventarioSistema.SubtrairItem(pocaoDeVida, 1);

            // Cura o jogador
            atributosPlayer.ReceberCura(50);
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