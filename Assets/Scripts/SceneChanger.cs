using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string nomeDaCena;

    public void MudarCena()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
