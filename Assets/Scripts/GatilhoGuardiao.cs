using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatilhoGuardiao : MonoBehaviour
{

    [Header("Identificação")]
    [Tooltip("Dê um nome único. ex. Marmota_001")]
    public string idUnico;

    [Header("Formação Inimiga")]
    [Tooltip("Arraste os PREFABS dos inimigos da aba PROJECT para cá")]
    public List<GameObject> inimigos;

    private void Start()
    {
        //Verifica a lista de inimigos derrotados
        if(DadosGlobais.inimigosDerrotados.Contains(idUnico))
        {
            //Desliga o inimigo caso ele tenha sido derrotado
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IniciadorBatalha iniciador = GetComponent<IniciadorBatalha>();

            //Extraor de niveis dos inimigos
            List<int> niveisExtraidos = new List<int>();
            AtributosCombate[] inimigosCena = GetComponentsInChildren<AtributosCombate>();

            foreach(AtributosCombate inimigo in inimigosCena)
            {
                niveisExtraidos.Add(inimigo.nivel);
            }

            if (iniciador != null)
            {
                iniciador.DispararBatalha(collision.gameObject, idUnico, inimigos, niveisExtraidos);
            }
        }
    }
}
