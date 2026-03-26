using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoedaColetavel : MonoBehaviour
{
    [Header("Configuração")]
    [Tooltip("Quantidade de ouro que esta moeda vale ao ser coletada")]
    public int valorDaMoeda = 1;

    public string idUnico;

    private void Start()
    {
        if (DadosGlobais.itensColetados.Contains(idUnico))
        {
            gameObject.SetActive(false);
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DadosGlobais.itensColetados.Add(idUnico);
        if (collision.CompareTag("Player"))
        {
            if (DadosGlobais.QuestAtiva != null)
            {
                if (DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.CacarMonstros ||
                    DadosGlobais.QuestAtiva.tipoMissao == TipoQuest.ColetarItens)
                {
                    DadosGlobais.progressoAtual++;
                    Debug.Log($"Quest: {DadosGlobais.progressoAtual}/{DadosGlobais.QuestAtiva.quantidade}");
                }
            }
            DadosGlobais.moedasAtualJogador += valorDaMoeda;
            GerenciadorDeAudio.instance.SomColeta(); //Audio de coleta
            Destroy(gameObject); // Some da tela!
        }
    }
}