using UnityEngine;

public class ProgressoJogador : MonoBehaviour
{
    public int xpAtual = 0;

    [Header("Tabela de XP")]
    public int[] xpNecessariaPorNivel = new int[] {100, 250, 500, 1000, 5000};

    private AtributosCombate atributos;

    private void Start()
    {
        atributos = GetComponent<AtributosCombate>();

        //Se tem dados armazenados na memoria gloval, ultilize
        if(DadosGlobais.nivelAtualJogador > 1 || DadosGlobais.xpAtualJogador > 0)
        {
            atributos.nivel = DadosGlobais.nivelAtualJogador;
            xpAtual = DadosGlobais.xpAtualJogador;

            //Força o recalculo do HP e Dano
            atributos.CalcularStatus();
        }
    }

    public void GanharXP(int quantidade)
    {
        xpAtual += quantidade;
        Debug.Log($"Voce ganhou {quantidade} de XP! Total: {xpAtual}");

        //Se o heroi nçao alcançou o nivel maximo
        if(atributos.nivel < xpNecessariaPorNivel.Length + 1)
        {
            //Verifica a lista: Se esta no nivel 1, procura pela posição 0
            int metaXP = xpNecessariaPorNivel[atributos.nivel - 1];

            if(metaXP > 0 && xpAtual >= metaXP)
            {
                //Sobe de nivel
                LevelUP(metaXP);
            }
        }
    }
    void LevelUP(int metaXP)
    {
        atributos.nivel++;
        xpAtual -= metaXP;//Pega o cp usado e guarda a que sobrou

        //Recalcula os atributos e atualiza a barra de vida
        atributos.CalcularStatus() ;
        atributos.hpAtual = atributos.hpMaximo;
        atributos.AtualizarBarra();

        Debug.Log($"LEVEL UP! O Heroi alcançou o nivel {atributos.nivel} !");

        //Verifica se o player subiu mais de 1 nivel por vez
        GanharXP(0);
    }
}
