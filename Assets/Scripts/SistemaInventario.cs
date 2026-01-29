using UnityEngine;
using System.Collections.Generic;

public class SistemaInventario : MonoBehaviour
{
public List<SlotInventario> inventario = new List<SlotInventario> ();

    public void AdicionarItem(DadosItem ItemParaAdicionar, int quantidade)
    {
        //1. Verificar se o item é impilhavel
        if (ItemParaAdicionar.ehEmpilavel|| quantidade <= 0)
        {
            //1.1 checar se ja existe um objeto desse tipo no inventario
            for(int i = 0; i < inventario.Count; i++)
            {
                if(inventario[i].dadosDoItem == ItemParaAdicionar)
                {
                    inventario[i].AdicionarQuantidade(quantidade);
                    Debug.Log($"Adicionado + {quantidade} ao item {ItemParaAdicionar.nomeDoItem}");

                    return;
                }
            }
        }

        //2. Item não empilhavel ou ainda não possui um igual
        //Criando um novo slot
        SlotInventario novoSlot = new SlotInventario(ItemParaAdicionar, quantidade);

        //Adicionado o slot ao inventario
        inventario.Add(novoSlot);
    }
    public void SubtrairItem(DadosItem item, int quantidade)
    {
        //1. Verifica se o item existe no inventario
        foreach(SlotInventario slot in inventario)
        {
            if (slot.dadosDoItem == item)
            {
                //1.1  subtrai a quantidade desejada de itens
                slot.SubtrairQuantidade(quantidade);
                Debug.Log($"Subtraido {quantidade} ao item {item.nomeDoItem}. Total: {slot.quantidade}");
                if (slot.quantidade <= 0)
                {
                    //Remove o item do inventario
                    inventario.Remove(slot);
                    Debug.Log($"Removido: {item.nomeDoItem}");
                    return;
                }
            }
        }
    }
}
