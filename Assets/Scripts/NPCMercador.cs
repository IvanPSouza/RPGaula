using TMPro;
using UnityEngine;

public class NPCMercador : MonoBehaviour
{
    [Header("Interface da loja")]
    public GameObject painelLoja;
    public TextMeshProUGUI textoFeedback;

    [Header("Inventario")]
    public SistemaInventario inventario;
    public DadosItem nutrientes;
    public int precoNutrientes;
    public int nutriComprados;

    private bool jogadorPerto;

    private void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            AbrirLoja();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.C))
        {
            FecharLoja();
        }
    }

    public void AbrirLoja()
    {
        painelLoja.SetActive(true);
        textoFeedback.text = "Seja bem-vinda!\ngostaria de alguns nutrientes?";
        nutriComprados = 0;
    }

    public void FecharLoja()
    {
        painelLoja.SetActive(false);
        nutriComprados = 0;
    }

    public void ComprarNutrientes()
    {
        //1. Verificar se o player tem dinheiro suficiente
        if(DadosGlobais.moedasAtualJogador >= precoNutrientes)
        {
            //2. Player tem dinheiro. Cobramos o valor
            DadosGlobais.moedasAtualJogador -= precoNutrientes;

            //3. Entrega o item
            if (DadosGlobais.QuantidadeNutrientes == 5)
            {
                inventario.AdicionarItem(nutrientes, 5);
                DadosGlobais.QuantidadeNutrientes = 1;
                //4. Exibir o feedback da compra
                textoFeedback.text = $"Nutrientes comprados com sucesso! Esta é a única vez que vou te dar 5 por esse preço." /*+ $" Saldo atal: {DadosGlobais.moedasAtualJogador}"*/;
            }
            else
            {
                nutriComprados++;
                inventario.AdicionarItem(nutrientes, 1);
                //4. Exibir o feedback da compra
                if(nutriComprados == 1)
                {
                    textoFeedback.text = $"Nutriente comprado com sucesso!" /*+ $" Saldo atal: {DadosGlobais.moedasAtualJogador}"*/;
                }
                else
                {
                    textoFeedback.text = $"{nutriComprados} Nutrientes comprados com sucesso!" /*+ $" Saldo atal: {DadosGlobais.moedasAtualJogador}"*/;
                }

            }
        }
        else
        {
            //Player sem dinheiro
            textoFeedback.text = $"Ouro insuficiente";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            jogadorPerto=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = false;
            painelLoja.SetActive(false);
        }
    }
}
