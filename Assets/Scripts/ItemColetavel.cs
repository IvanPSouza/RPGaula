using System.Collections.Generic;
using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public DadosItem item; //ScriptableObject
    public int quantidade = 1;

    public string idUnico;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (DadosGlobais.itensColetados.Contains(idUnico))
        {
            gameObject.SetActive(false);
            return;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(item != null)
        {
            spriteRenderer.sprite = item.icone;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Procura pelo inventario no gerenciador do jogo
            SistemaInventario inventario = FindFirstObjectByType<SistemaInventario>();

            if (inventario != null)
            {
                inventario.AdicionarItem(item, quantidade);
                Destroy(gameObject);
            }
            //Empacotando o prefab do inimigo em uma lista
            DadosGlobais.itensColetados.Add(idUnico);
        }
    }
}
