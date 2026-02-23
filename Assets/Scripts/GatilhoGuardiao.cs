using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatilhoGuardiao : MonoBehaviour
{
    [Header("Formação Inimiga")]
    [Tooltip("Arraste os PREFABS dos inimigos da aba PROJECT para cá")]
    public List<GameObject> inimigosDesteGrupo;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            // Passa a lista inteira do grupo para a Memória Global
            DadosGlobais.prefabsInimigos = new List<GameObject>(inimigosDesteGrupo);
            SceneManager.LoadScene("CenaBatalha");
        }
    }
}
