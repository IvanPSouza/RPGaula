using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeAudio : MonoBehaviour
{
    public static GerenciadorDeAudio instance;

    [Header("Sources")]
    public AudioSource fonteMusica;
    public AudioSource fonteSFX;

    [Header("UI (opcional)")]
    public Slider sliderMusica;
    public Slider sliderSFX;

    [Header("Clipes")]
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

    private void Start()
    {
        // Carrega volumes salvos
        float volMusica = PlayerPrefs.GetFloat("volMusica", fonteMusica.volume);
        float volSFX = PlayerPrefs.GetFloat("volSFX", fonteSFX.volume);

        fonteMusica.volume = volMusica;
        fonteSFX.volume = volSFX;

        // Se sliders existirem, sincroniza
        if (sliderMusica != null)
        {
            sliderMusica.value = volMusica;
            sliderMusica.onValueChanged.AddListener(SetVolumeMusica);
        }

        if (sliderSFX != null)
        {
            sliderSFX.value = volSFX;
            sliderSFX.onValueChanged.AddListener(SetVolumeSFX);
        }
    }

    // Método para reconectar sliders quando mudar de cena
    public void SetSliders(Slider musica, Slider sfx)
    {
        sliderMusica = musica;
        sliderSFX = sfx;

        if (sliderMusica != null)
        {
            sliderMusica.value = fonteMusica.volume;
            sliderMusica.onValueChanged.RemoveAllListeners();
            sliderMusica.onValueChanged.AddListener(SetVolumeMusica);
        }

        if (sliderSFX != null)
        {
            sliderSFX.value = fonteSFX.volume;
            sliderSFX.onValueChanged.RemoveAllListeners();
            sliderSFX.onValueChanged.AddListener(SetVolumeSFX);
        }
    }

    public void SetVolumeMusica(float volume)
    {
        fonteMusica.volume = volume;
        PlayerPrefs.SetFloat("volMusica", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        fonteSFX.volume = volume;
        PlayerPrefs.SetFloat("volSFX", volume);
    }

    // ================== ÁUDIO ==================

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

    public void SomColeta() => TocarSXF(somColeta);
    public void SomPunch() => TocarSXF(somPunch);
    public void SomFlecha() => TocarSXF(somFlecha);
    public void SomHeal() => TocarSXF(somHeal);
    public void SomMordida() => TocarSXF(somMordida);
    public void SomClique() => TocarSXF(somClique);
    public void SomLvlUp() => TocarSXF(somLevelUp);
    public void SomDeath() => TocarSXF(somDeath);
    public void SomBlaBlaBla() => TocarSXF(somBlaBlaBla);
}