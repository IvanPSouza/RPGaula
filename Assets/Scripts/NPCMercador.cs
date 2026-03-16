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

    private bool jogadorPerto;

    private void Update()
    {
        if(jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            AbrirLoja();
        }
    }

    public void AbrirLoja()
    {
        painelLoja.SetActive(true);
        textoFeedback.text = "Seja bem-vinda!\ngostaria de alguns nutrientes?";
    }

    public void FecharLoja()
    {
        painelLoja.SetActive(false);
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
                textoFeedback.text = $"Nutrientes comprados com sucesso! essa e a unica vez que vou te dar 5 por esse preço. " + $"Saldo atal: {DadosGlobais.moedasAtualJogador}";
            }
            else
            {
                inventario.AdicionarItem(nutrientes, 1);
                //4. Exibir o feedback da compra
                textoFeedback.text = $"Nutriente comprado com sucesso! " + $"Saldo atal: {DadosGlobais.moedasAtualJogador}";
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
