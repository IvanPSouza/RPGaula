using System.Collections.Generic;
using UnityEngine;

public class GerenciadorBatalha : MonoBehaviour
{
    public Transform pontoHeroi;
    public Transform[] pontosInimigos;
    public GameObject prefabHeroi;

    void Start()
    {
        // 1. Cria o Herói e desliga o script de exploração
        GameObject heroi = Instantiate(prefabHeroi, pontoHeroi.position, Quaternion.identity);

        AtributosCombate atributosJogador = heroi.GetComponent<AtributosCombate>();
        atributosJogador.nivel = DadosGlobais.nivelAtualJogador;
        atributosJogador.CalcularStatus();

        if (heroi.GetComponent<MovimentacaoEploracao>() != null)
            heroi.GetComponent<MovimentacaoEploracao>().enabled = false;

        // 2. Cria os inimigos automaticamente
        List<GameObject> grupoPrefabs = DadosGlobais.prefabsInimigos;

        for (int i = 0; i < grupoPrefabs.Count; i++)
        {
            if (i >= pontosInimigos.Length) break;

            // Instancia diretamente a versão "Arena" do monstro!
            // Zero IFs e zero necessidade de desativar IA!
            GameObject inimigo = Instantiate(grupoPrefabs[i], pontosInimigos[i].position, Quaternion.identity);


            //Pega os dados da memoria global e atribui aos inimigos
            AtributosCombate atributos = inimigo.GetComponent<AtributosCombate>();

            if(i < DadosGlobais.niveisInimigosArena.Count)
            {
                atributos.nivel = DadosGlobais.niveisInimigosArena[i];
                atributos.CalcularStatus();
                atributos.hpAtual = atributos.hpMaximo;
            }
        }
    }
}
