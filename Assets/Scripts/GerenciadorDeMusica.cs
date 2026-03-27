using UnityEngine;

public class GerenciadorDeMusica : MonoBehaviour
{
    public AudioClip MusicaAoIniciarTocar;
    void Start()
    {
        GerenciadorDeAudio.instance.TocarMusica(MusicaAoIniciarTocar); //Audio de coleta
    }
}
