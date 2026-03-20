using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDerrota : MonoBehaviour
{
    public void BotaoJogarNovamente(string CenaBatalha)
    {
        SceneManager.LoadScene(CenaBatalha);
    }
}
