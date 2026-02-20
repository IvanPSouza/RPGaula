using UnityEngine;

public class GerenciadorBatalha : MonoBehaviour
{
    [Header("Posições")]
    public Transform pontoInimigo;  // Arraste Posicao_Inimigo
    public Transform pontoHeroi;    // Arraste Posicao_Heroi

    [Header("Prefabs")]
    public GameObject prefabMole;   // Arraste o Prefab do Mole
    public GameObject prefabTreant; // Arraste o Prefab do Treant
    public GameObject prefabHeroi;  // Arraste o Prefab do Player

    void Start()
    {
        // 1. CRIA O HERÓI E GUARDA A REFERÊNCIA
        GameObject heroi = Instantiate(prefabHeroi, pontoHeroi.position, Quaternion.identity);

        // --- DESATIVA O MOVIMENTO DO HERÓI ---
        if (heroi.GetComponent<MovimentacaoEploracao>() != null)
        {
            heroi.GetComponent<MovimentacaoEploracao>().enabled = false;
        }

        // 2. LÊ QUAL INIMIGO CRIAR
        string inimigo = DadosGlobais.inimigoParaGerar;
        GameObject monstroCriado = null; // Variável para guardar o inimigo

        if (inimigo == "Mole")
            monstroCriado = Instantiate(prefabMole, pontoInimigo.position, Quaternion.identity);
        else if (inimigo == "Treant")
            monstroCriado = Instantiate(prefabTreant, pontoInimigo.position, Quaternion.identity);

        // 3. DESATIVA A IA DO INIMIGO
        if (monstroCriado != null)
        {
            // Desliga o script que faz ele patrulhar e perseguir
            monstroCriado.GetComponent<ControladorInimigo>().enabled = false;
        }
    }
}
