using UnityEngine;

public class GerenciadorDeAudio : MonoBehaviour
{
    public static GerenciadorDeAudio instance;
    public AudioSource fonteMusica;
    public AudioSource fonteSFX;

    public AudioClip somColeta;
    public AudioClip somPunch;
    public AudioClip somFlecha;
    public AudioClip somMordida;
    public AudioClip somHeal;
    public AudioClip somClique;
    public AudioClip somLevelUp;
    public AudioClip somDeath;
    public AudioClip somBlaBlaBla;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TocarMusica(AudioClip musica)
    {
        if (fonteMusica.clip == musica) return;
        fonteMusica.clip = musica;
        fonteMusica.Play();
    }
    public void TocarSXF(AudioClip efeitoSonoro)
    {
        fonteSFX.PlayOneShot(efeitoSonoro);
    }
    public void SomColeta()
    {
        TocarSXF(somColeta);
    }
    public void SomPunch()
    {
        TocarSXF(somPunch);
    }
    public void SomFlecha()
    {
        TocarSXF(somFlecha);
    }
    public void SomHeal()
    {
        TocarSXF(somHeal);
    }
    public void SomMordida()
    {
        TocarSXF(somMordida);
    }
    public void SomClique()
    {
        TocarSXF(somClique);
    }
    public void SomLvlUp()
    {
        TocarSXF(somLevelUp);
    }
    public void SomDeath()
    {
        TocarSXF(somDeath);
    }
    public void SomBlaBlaBla()
    {
        TocarSXF(somBlaBlaBla);
    }
}
