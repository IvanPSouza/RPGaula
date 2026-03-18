using UnityEngine;

public class CueioMorto : MonoBehaviour
{
    public bool Foi = false;
    // Update is called once per frame
    void Update()
    {
        if(DadosGlobais.inimigosDerrotados.Contains("Cueio") && Foi == false)
        {
            DadosGlobais.progressoAtual = 0;
            DadosGlobais.progressoAtual++;
            Foi = true;
        }
    }
}
