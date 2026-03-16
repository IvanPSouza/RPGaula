using UnityEngine;
using System.Collections.Generic;
using System;

public class SistemaInventario : MonoBehaviour
{
    public List<SlotInventario> inventario = new List<SlotInventario> ();

    [Header("Economia")] 
    public int moedas = 0;

    //Evento que indica alteração no inventario
    public event Action onInventarioMudou;

    // Adicione no topo do SistemaInventario.cs
    void Awake()
    {
        // Se a memória global estiver vazia (início do jogo), salva os itens iniciais do Inspector nela
        if (DadosGlobais.inventarioAtual.Count == 0 && inventario.Count > 0)
        {
            DadosGlobais.inventarioAtual = new List<SlotInventario>(inventario);
            moedas = DadosGlobais.moedasAtualJogador;
        }

        // Passagem por Referência
        inventario = DadosGlobais.inventarioAtual;

        //Sempre que voltar ao mundo de exploração, captura o valor armazenado nos DadosGlobais
        moedas = DadosGlobais.moedasAtualJogador;
    }

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

                    //Informa a Unity que ocorreu uma alteração no inventário
                    if(onInventarioMudou != null)
                    {
                        onInventarioMudou.Invoke();
                    }

                    return;
                }
            }
        }

        //2. Item não empilhavel ou ainda não possui um igual
        //Criando um novo slot
        SlotInventario novoSlot = new SlotInventario(ItemParaAdicionar, quantidade);

        //Adicionado o slot ao inventario
        inventario.Add(novoSlot);

        //Informa a unity que uma alteração
        if (onInventarioMudou != null)
        {
            onInventarioMudou.Invoke();
        }
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
                if (onInventarioMudou != null)
                {
                    onInventarioMudou.Invoke();
                }
                if (slot.quantidade <= 0)
                {
                    //Remove o item do inventario
                    inventario.Remove(slot);
                    Debug.Log($"Removido: {item.nomeDoItem}");

                    //Informa a Unity que ocorreu uma alteração no inventario
                    if(onInventarioMudou!= null)
                    {
                        onInventarioMudou.Invoke();
                    }
                    return;
                }
            }
        }
    }

    public void ModificadorMoedas(int valor)
    {
        moedas += valor;
        if(moedas < 0)
        {
            moedas = 0;
        }

        //Informa a Unity que ocorreu uma alteração no inventario
        if (onInventarioMudou != null)
        {
            onInventarioMudou.Invoke();
        }
    }

    public bool TemItem(DadosItem item, int qtd)
    {
        //Verifica se tem oitem e quanidade necessaria dentro do inventario
        foreach(SlotInventario slot in inventario)
        {
            if(slot.dadosDoItem == item && slot.quantidade >= qtd)
            {
                return true;
            }
        }

        return false;
    }

    private void OnValidate()
    {
        if(Application.isPlaying && onInventarioMudou != null)
        {
            onInventarioMudou.Invoke();
        }
    }
}
