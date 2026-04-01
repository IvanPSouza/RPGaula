using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    // Nome da cena que será carregada

    // Função para trocar de cena
    public void TrocarCena(string nomeDaCena)
    {
        if (!string.IsNullOrEmpty(nomeDaCena))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nomeDaCena);
        }
        else
        {
            Debug.LogWarning("Nome da cena não foi definido!");
        }
    }

    // Função para fechar o jogo
    public void FecharJogo()
    {
        Debug.Log("Fechando o jogo...");

        // Isso aqui é só para funcionar no Editor da Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}