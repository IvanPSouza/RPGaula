using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciadorBatalha : MonoBehaviour
{
    public void DispararBatalha(GameObject player, string idInimigo, List<GameObject> listaInimigos)
    {
        //1. Salva a posição do jogador o mundo
        DadosGlobais.posicaoRetornoJogador = player.transform.position;

        //2. Salva a identidade e a formação dos inimigos
        DadosGlobais.idInimigoEmCombate = idInimigo;
        DadosGlobais.prefabsInimigos = new List<GameObject>(listaInimigos);

        //3. Carrega arena de combate
        SceneManager.LoadScene("CenaBatalha");
    }
}
