using System.Collections;
using UnityEngine;

public class EefeitoCamera : MonoBehaviour
{
    //Singleton
    public static EefeitoCamera instance;

    private Vector3 posicaoOriginal;

    private void Awake()
    {
        instance = this;
        //Armazena a posição original da camera antes de aplicar o efeito
        posicaoOriginal = transform.localPosition;
    }

    public void TremerTela(float duracao, float forca)
    {
        StartCoroutine(RotinaTremor(duracao, forca));
    }
    IEnumerator RotinaTremor(float duracao, float forca)
    {
        float tempoPassado = 0f;

        while (tempoPassado < duracao)
        {
            float x = Random.Range(-1, 1) * forca;
            float y = Random.Range(-1, 1) * forca;

            transform.localPosition = new Vector3(x, y, posicaoOriginal.z);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicaoOriginal;
    }
}
