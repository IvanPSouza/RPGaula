using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FichaStatus : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI txtNome;
    public TextMeshProUGUI txtNivel;
    public TextMeshProUGUI txtAtaque;
    public TextMeshProUGUI txtVida;
    public TextMeshProUGUI txtXP;

    [Header("Barras")]
    public Slider barraXP;
    public Slider barraVida;

    private ProgressoJogador progresso;
    private AtributosCombate atributos;

    private void Start()
    {
        // O painel começa ativo só pra pegar os dados
        gameObject.SetActive(true);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            progresso = player.GetComponent<ProgressoJogador>();
            atributos = player.GetComponent<AtributosCombate>();
        }
        else
        {
            Debug.LogError("Player não encontrado!");
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        AtualizarFicha();
    }

    public void AtualizarFicha()
    {
        if (atributos == null || progresso == null) return;

        txtNome.text = $"Nome: Cenoura";
        txtNivel.text = $"Nivel: {atributos.nivel}";

        txtAtaque.text = $"Dano Base: {atributos.danoAtual}";
        txtVida.text = $"{atributos.hpAtual}/{atributos.hpMaximo}";

        // Barra de vida
        barraVida.maxValue = atributos.hpMaximo;
        barraVida.value = atributos.hpAtual;

        // XP agora funciona com níveis infinitos
        int metaXP = progresso.ObterXPProximoNivel();

        barraXP.maxValue = metaXP;
        barraXP.value = progresso.xpAtual;

        txtXP.text = $"{progresso.xpAtual} / {metaXP}";
    }
}