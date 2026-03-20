using UnityEngine;
using UnityEngine.UI;

public class VidaInventario : MonoBehaviour
{
    public Slider sliderVida;

    private AtributosCombate atributos;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player não encontrado!");
            return;
        }

        atributos = player.GetComponent<AtributosCombate>();

        if (atributos == null)
        {
            Debug.LogError("AtributosCombate não encontrado no Player!");
            return;
        }

        AtualizarBarra();
    }

    void Update()
    {
        AtualizarBarra();
    }

    void AtualizarBarra()
    {
        if (atributos == null) return;

        sliderVida.maxValue = atributos.hpMaximo;
        sliderVida.value = atributos.hpAtual;
    }
}
