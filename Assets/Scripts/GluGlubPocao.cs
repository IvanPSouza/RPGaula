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

        inventarioSistema = FindFirstObjectByType<SistemaInventario>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)|| Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape))
        {
            textoFeedback.text = "...";
        }
    }

    public void UsarPocao()
    {
        if (atributosPlayer == null || inventarioSistema == null) return;

        if (atributosPlayer.hpAtual >= atributosPlayer.hpMaximo)
        {
            GerenciadorDeAudio.instance.SomClique(); //Audio
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
            GerenciadorDeAudio.instance.SomHeal(); //Audio
            DadosGlobais.hpAtualJogador = atributosPlayer.hpAtual;

            if (textoFeedback != null)
                textoFeedback.text = "Poção usada!";
        }
        else
        {
            GerenciadorDeAudio.instance.SomClique(); //Audio
            if (textoFeedback != null)
                textoFeedback.text = "Sem poções!";
        }
    }
}