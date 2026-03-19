using TMPro;
using UnityEngine;

public class ControleUI : MonoBehaviour
{
    [Header("Paineis")]
    public GameObject PainelInventario;
    public GameObject PainelCrafting;
    public GameObject PainelPause;

    public TextMeshProUGUI textoFeedbackCraft;

    private bool estaPausado = false;

    void Update()
    {
        // INVENTÁRIO
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (estaPausado) return;

            PainelCrafting.SetActive(false);
            PainelInventario.SetActive(!PainelInventario.activeSelf);
        }

        // CRAFTING
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (estaPausado) return;

            textoFeedbackCraft.text = "...";
            PainelInventario.SetActive(false);
            PainelCrafting.SetActive(!PainelCrafting.activeSelf);
        }

        // FECHAR INVENTÁRIO/CRAFTING
        if (Input.GetKeyDown(KeyCode.E))
        {
            PainelInventario.SetActive(false);
            PainelCrafting.SetActive(false);
        }

        // PAUSE (ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!estaPausado)
            {
                // Só pausa se nenhum outro painel estiver aberto
                if (!PainelInventario.activeSelf && !PainelCrafting.activeSelf)
                {
                    Pausar();
                }
                else
                {
                    // Se algum estiver aberto, apenas fecha eles
                    PainelInventario.SetActive(false);
                    PainelCrafting.SetActive(false);
                }
            }
            else
            {
                Despausar();
            }
        }
    }

    void Pausar()
    {
        PainelPause.SetActive(true);
        Time.timeScale = 0f;
        estaPausado = true;
    }

    void Despausar()
    {
        PainelPause.SetActive(false);
        Time.timeScale = 1f;
        estaPausado = false;
    }

    // Botão de fechar no painel de pause
    public void BotaoFecharPause()
    {
        Despausar();
    }
}