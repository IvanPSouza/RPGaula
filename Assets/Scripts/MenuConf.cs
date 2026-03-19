using UnityEngine;

public class MenuConf : MonoBehaviour
{
    public GameObject Menu1;
    public GameObject MenuConfi;

    // Chamado pelo botão 1
    public void AtivarA()
    {
        Menu1.SetActive(true);
        MenuConfi.SetActive(false);
    }

    // Chamado pelo botão 2
    public void AtivarB()
    {
        Menu1.SetActive(false);
        MenuConfi.SetActive(true);
    }
}
