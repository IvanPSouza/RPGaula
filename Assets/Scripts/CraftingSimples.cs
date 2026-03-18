using UnityEngine;
using TMPro;

public class CraftingSimples : MonoBehaviour
{
    public SistemaInventario inventario;

    [Header("Itens Necessarios")]
    public DadosItem Componentes;

    [Header("Item Craftado")]
    public DadosItem Sumo;

    public int custo = 1;
    public int quantidadeProduzida = 5;
    public TextMeshProUGUI textoFeedback;
    public string NomeDoItem;
    private int somatorio = 0;

    public void CraftarSumo()
    {
        if (inventario.TemItem(Componentes, custo))
        {
            somatorio += quantidadeProduzida;
            inventario.SubtrairItem(Componentes, custo);
            inventario.AdicionarItem(Sumo, quantidadeProduzida);
            textoFeedback.text = $"Sucesso! {somatorio} {NomeDoItem} criadas";
        }
        else
        {
            textoFeedback.text = "Falha: voce não tem os itens necessarios";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            somatorio = 0;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            somatorio = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            somatorio = 0;
        }
    }
}
