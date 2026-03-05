using System.Collections.Generic;
using UnityEngine;

public class EncontroAleatorio : MonoBehaviour
{

    [Header("Identificação")]
    [Tooltip("Dê um nome único. ex. Marmota_001")]
    public string idUnico;

    [Header("Formação Inimiga")]
    [Tooltip("Arraste os PREFABS dos inimigos da aba PROJECT para cá")]
    public List<GameObject> inimigos;

    [Header("Chance de encontro")]
    public int chanceMax = 1000;

    private void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int numero = Random.Range(1, chanceMax + 1);
            IniciadorBatalha iniciador = GetComponent<IniciadorBatalha>();

            if (numero == 1)
            {
                Debug.Log("foi dessa vez");
                if (iniciador != null)
                {
                    //Extraor de niveis dos inimigos
                    List<int> niveisExtraidos = new List<int>();
                    AtributosCombate[] inimigosCena = GetComponentsInChildren<AtributosCombate>();
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
