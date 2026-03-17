using UnityEngine;

public class MapaMissoes : MonoBehaviour
{
    public GameObject BarreiraFungica1;
    public GameObject BarreiraFungica1A;
    public GameObject BarreiraFungica2;


    public GameObject Moedas1;
    public GameObject Cebola;

    //public MonoBehaviour scriptParaAtivar;

    void Update()
    {
        int missoes = DadosGlobais.missoesConcluidas;

        if (missoes >= 1)
        {
            Moedas1.SetActive(true);
            //scriptParaAtivar.enabled = true;
        }

        if (missoes >= 2)
        {
            BarreiraFungica1.SetActive(false);
            Cebola.SetActive(true);
        }
        if (missoes >= 3)
        {
            BarreiraFungica1A.SetActive(false);
        }
        if (missoes >= 4)
        {
            BarreiraFungica2.SetActive(false);
        }
    }
}
