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
                    if(inventario[i].quantidade <= 0)
                    {
                        inventario.RemoveAt(i);
                    }
                    return;
                }
            }
        }

        //2. Item não empilhavel ou ainda não possui um igual
        //Criando um novo slot
        if (quantidade <= 0)
        {
            return;
        }
        SlotInventario novoSlot = new SlotInventario(ItemParaAdicionar, quantidade);

        //Adicionado o slot ao inventario
        inventario.Add(novoSlot);
    }
}
