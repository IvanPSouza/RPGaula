using UnityEngine;
using UnityEngine.UI;

public class AtributosCombate : MonoBehaviour
{
    public string nomePersonagem;
    public int hpMaximo = 100;
    public int hpAtual;
    public int danoBase = 10;

    [Header("UI")]
    public Slider minhaBarraDeVida;

    void Start()
    {
        if(gameObject.CompareTag("Player"))
        {
            if(DadosGlobais.hpAtualJogador != -1)
            {
                hpAtual = DadosGlobais.hpAtualJogador;
            }
            else
            {
                hpAtual = hpMaximo;
            }
        }
        else
        {
            hpAtual = hpMaximo;
        }

            // hpAtual = hpMaximo;
            AtualizarBarra();
    }

    public void ReceberDano(int valorDano)
    {
        hpAtual -= valorDano;
        AtualizarBarra();
        Debug.Log(nomePersonagem + " recebeu " + valorDano + " de dano! HP: " + hpAtual);

        if (hpAtual <= 0)
        {
            hpAtual = 0;
            gameObject.SetActive(false);
        }
    }

    public void ReceberCura(int valorCura)
    {
        hpAtual += valorCura;

        Debug.Log(nomePersonagem + " recebeu " + valorCura + " de cura!|HP: " + hpAtual);

        // Impede que a vida ultrapasse o máximo!
        if (hpAtual >= hpMaximo) hpAtual = hpMaximo;//Evita valores superiores ao HP Maximo

        AtualizarBarra();
    }

    public void AtualizarBarra()
    {
        if (minhaBarraDeVida != null)
        {
            minhaBarraDeVida.maxValue = hpMaximo;
            minhaBarraDeVida.value = hpAtual;
        }
    }
}