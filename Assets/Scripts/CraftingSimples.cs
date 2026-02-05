using UnityEngine;

public class CraftingSimples : MonoBehaviour
{
    public SistemaInventario inventario;

    [Header("Itens Necessarios")]
    public DadosItem Madeira;

    [Header("Item Craftado")]
    public DadosItem Flecha;

    public int custo = 1;
    public int quantidadeProduzida = 5;

    public void CraftarFlechas()
    {
        if (inventario.TemItem(Madeira, custo))
        {
            inventario.SubtrairItem(Madeira, custo);
            inventario.AdicionarItem(Flecha, quantidadeProduzida);
            Debug.Log($"Sucesso! {quantidadeProduzida} x flechas criadas");
        }
        else
        {
            Debug.Log("Falha: voce não tem os itens necessarios");
        }
    }
}
