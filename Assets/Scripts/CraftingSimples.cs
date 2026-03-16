using UnityEngine;

public class CraftingSimples : MonoBehaviour
{
    public SistemaInventario inventario;

    [Header("Itens Necessarios")]
    public DadosItem Componentes;

    [Header("Item Craftado")]
    public DadosItem Sumo;

    public int custo = 1;
    public int quantidadeProduzida = 5;

    public void CraftarSumo()
    {
        if (inventario.TemItem(Componentes, custo))
        {
            inventario.SubtrairItem(Componentes, custo);
            inventario.AdicionarItem(Sumo, quantidadeProduzida);
            Debug.Log($"Sucesso! {quantidadeProduzida} x sumos criados");
        }
        else
        {
            Debug.Log("Falha: voce não tem os itens necessarios");
        }
    }
}
