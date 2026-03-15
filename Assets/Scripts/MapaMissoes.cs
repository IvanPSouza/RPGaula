using UnityEngine;

public class MapaMissoes : MonoBehaviour
{
    public GameObject BarreiraFungica1;
    public GameObject BarreiraFungica2;

    public GameObject Moedas1;

    void Update()
    {
        int missoes = DadosGlobais.missoesConcluidas;

        if (missoes >= 1)
        {
            Moedas1.SetActive(true);
        }

        if (missoes >= 2)
        {
            BarreiraFungica1.SetActive(false);
        }

        if (missoes >= 4)
        {
            BarreiraFungica2.SetActive(false);
        }
    }
}
