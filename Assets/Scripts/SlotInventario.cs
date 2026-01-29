using UnityEngine;

[System.Serializable] //OBRIGATORIO para visualizar no Inspector
public class SlotInventario
{
    public DadosItem dadosDoItem;
    public int quantidade;

    //Construtor
    public SlotInventario(DadosItem item, int gtd)
    {
        dadosDoItem = item;
        quantidade = gtd;
    }

    public void AdicionarQuantidade(int qtd)
    {
        quantidade += qtd;
    }
    public void SubtrairQuantidade(int qtd)
    {
        quantidade -= qtd;
    }
}
