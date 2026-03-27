using UnityEngine;

public class ApertaBotao : MonoBehaviour
{
    public AudioClip click;
    public void Clique()
    {
        GerenciadorDeAudio.instance.TocarSXF(click);
    }
}
