using TMPro;
using UnityEngine;

public class interfaceInventario : MonoBehaviour
{
public SistemaInventario sistemaInventario;
    public Transform containerGrid;
    public GameObject prefabSlot;

    [Header("Economia")]
    public TextMeshProUGUI textoMoedas1;
    public TextMeshProUGUI textoMoedas2;

    private void Start()
    {
        //Inscrição no sistema de Eventos
        //Senore que o inventario mudar, a interface redesenha automaticamente
        sistemaInventario.onInventarioMudou += AtualizarInterface;

        //Atualizar o inventario ao começar o jogo
        AtualizarInterface();
    }

    void Update()
    {
        //O Inventário agora lê a fortuna global do jogador o tempo todo!
        if (textoMoedas1 != null)
        {
            textoMoedas1.text = "Ouro: " + DadosGlobais.moedasAtualJogador.ToString();
        }
        if (textoMoedas2 != null)
        {
            textoMoedas2.text = "Ouro: " + DadosGlobais.moedasAtualJogador.ToString();
        }
    }

    public void AtualizarInterface()
    {
        //1. Atualiza as Moedas
        if(textoMoedas1 != null)
        {
            textoMoedas1.text = "Ouro:" + sistemaInventario.moedas.ToString();
        }
        if (textoMoedas2 != null)
        {
            textoMoedas2.text = "Ouro:" + sistemaInventario.moedas.ToString();
        }

        //2. Limpeza do grid
        foreach (Transform item in containerGrid)
        {
            Destroy(item.gameObject);
        }

        //3. Monta o inventario
        foreach(SlotInventario slot in sistemaInventario.inventario)
        {
            GameObject novoSlot = Instantiate(prefabSlot, containerGrid);
            novoSlot.GetComponent<SlotUI>().ConfigurarSlot(slot);
        }
    }
}
