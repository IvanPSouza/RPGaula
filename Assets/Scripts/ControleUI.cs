using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ControleUI : MonoBehaviour
{
    [Header("Paineis")]
    //Adicionar TODOS os paineis do jogo
    public GameObject PainelInventario;
    public GameObject PainelCrafting;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PainelCrafting.SetActive(false);
            //Inverte o estado do inventario, abre/fecha
            PainelInventario.SetActive(!PainelInventario.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PainelInventario.SetActive(false);
            //Inverte o estado do menu crafting, abre/fecha
            PainelCrafting.SetActive(!PainelCrafting.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PainelInventario.SetActive(false);
            PainelCrafting.SetActive(false);
        }
    }
}
