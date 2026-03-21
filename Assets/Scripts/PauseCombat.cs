using UnityEngine;

public class PauseCombat : MonoBehaviour
{
    public GameObject PainelPause;
    private bool estaPausado = false;

    // Update is called once per frame
    void Update()
    {
        // PAUSE (ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!estaPausado)
            {
                Pausar();
            }
            else
            {
                Despausar();
            }
        }
    }
        public void Pausar()
        {
            PainelPause.SetActive(true);
            Time.timeScale = 0f;
            estaPausado = true;
        }

        public void Despausar()
        {
            PainelPause.SetActive(false);
            Time.timeScale = 1f;
            estaPausado = false;
        }
    }
