using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoedaColetavel : MonoBehaviour
{
    [Header("Configuração")]
    [Tooltip("Quantidade de ouro que esta moeda vale ao ser coletada")]
    public int valorDaMoeda = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DadosGlobais.moedasAtualJogador += valorDaMoeda;
            Destroy(gameObject); // Some da tela!
        }
        if (DadosGlobais.QuestAtiva != null)
        {
            if (DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.CacarMonstros ||
                DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.ColetarItens)
            {
                DadosGlobais.progressoAtual++;
                Debug.Log($"Quest: {DadosGlobais.progressoAtual}/{DadosGlobais.QuestAtiva.quantidade}");
            }
        }
    }
}