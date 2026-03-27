using UnityEngine;
using UnityEngine.UI;

public class ConectarSlidersAudio : MonoBehaviour
{
    public Slider sliderMusica;
    public Slider sliderSFX;

    void Start()
    {
        if (GerenciadorDeAudio.instance != null)
        {
            GerenciadorDeAudio.instance.SetSliders(sliderMusica, sliderSFX);
        }
    }
}
