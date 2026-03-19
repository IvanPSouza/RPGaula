using System.Collections.Generic;
using UnityEngine;

public class EncontroAleatorio : MonoBehaviour
{
    [Header("Identificação")]
    public string idUnico;

    [Header("Formação Inimiga")]
    public List<GameObject> inimigos;

    [Header("Chance de encontro")]
    public int chanceMax = 500;

    // ===== CONTROLE DE TEMPO =====
    private float proximoCheck = 0f;
    public float intervalo = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ===== CONTROLE DE TEMPO (1 SEGUNDO) =====
            if (Time.time < proximoCheck)
                return;

            proximoCheck = Time.time + intervalo;

            int numero = Random.Range(1, chanceMax + 1);
            IniciadorBatalha iniciador = GetComponent<IniciadorBatalha>();

            if (numero == 1)
            {
                Debug.Log("foi dessa vez");

                if (iniciador != null)
                {
                    List<int> niveisExtraidos = new List<int>();
                    AtributosCombate[] inimigosCena = GetComponentsInChildren<AtributosCombate>();

                    foreach (AtributosCombate inimigo in inimigosCena)
                    {
                        niveisExtraidos.Add(inimigo.nivel);
                    }

                    iniciador.DispararBatalha(collision.gameObject, idUnico, inimigos, niveisExtraidos);
                }
            }
            else
            {
                Debug.Log("Não foi dessa vez");
            }
        }
    }
}