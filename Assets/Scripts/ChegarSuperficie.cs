using UnityEngine;
using UnityEngine.SceneManagement;

public class ChegarSuperficie : MonoBehaviour
{
    public string Cena;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(Cena);
        }
    }
}
