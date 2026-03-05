using UnityEngine;
using UnityEngine.UI;

public class AtributosCombate : MonoBehaviour
{
    [Header("Evolução")]
    public int nivel = 1;

    [Header("Status Base")]
    public int hpBase = 100;
    public int danoBase = 10;

    public string nomePersonagem;

    [Header("Status calculados")]
    public int hpMaximo = 100;
    public int hpAtual;
    public int danoAtual = 10;

    [Header("UI")]
    public Slider minhaBarraDeVida;

    void Start()
    {
        CalcularStatus();
        if(gameObject.CompareTag("Player") && DadosGlobais.hpAtualJogador != -1)
        {
            hpAtual = DadosGlobais.hpAtualJogador;
        }
        else
        {
            hpAtual = hpMaximo;
        }

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

    public void CalcularStatus()
    {
        //Ganha +20 de HP e +5 de dano por nivel
        hpMaximo = hpBase + ((nivel - 1) * 20);
        danoAtual = danoBase + ((nivel - 1) * 5);

        if(hpAtual > hpMaximo)
        {
            hpAtual = hpMaximo;
        }
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