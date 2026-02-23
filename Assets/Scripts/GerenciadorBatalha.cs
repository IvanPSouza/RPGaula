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

        if (heroi.GetComponent<MovimentacaoEploracao>() != null)
            heroi.GetComponent<MovimentacaoEploracao>().enabled = false;

        // 2. Cria os inimigos automaticamente
        List<GameObject> grupoPrefabs = DadosGlobais.prefabsInimigos;

        for (int i = 0; i < grupoPrefabs.Count; i++)
        {
            if (i >= pontosInimigos.Length) break;

            // Instancia diretamente a versão "Arena" do monstro!
            // Zero IFs e zero necessidade de desativar IA!
            Instantiate(grupoPrefabs[i], pontosInimigos[i].position, Quaternion.identity);
        }
    }
}
