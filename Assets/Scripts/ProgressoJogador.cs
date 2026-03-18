using UnityEngine;

public class ProgressoJogador : MonoBehaviour
{
    public int xpAtual = 0;

    [Header("Tabela de XP")]
    public int[] xpNecessariaPorNivel = new int[] { 100, 250, 500, 1000, 5000 };

    private AtributosCombate atributos;

    private void Start()
    {
        atributos = GetComponent<AtributosCombate>();

        // Se tem dados armazenados na memoria global, utilize
        if (DadosGlobais.nivelAtualJogador > 1 || DadosGlobais.xpAtualJogador > 0)
        {
            atributos.nivel = DadosGlobais.nivelAtualJogador;
            xpAtual = DadosGlobais.xpAtualJogador;

            // Força o recalculo do HP e Dano
            atributos.CalcularStatus();
        }
    }

    public void GanharXP(int quantidade)
    {
        xpAtual += quantidade;
        Debug.Log($"Voce ganhou {quantidade} de XP! Total: {xpAtual}");

        int metaXP = ObterXPProximoNivel();

        if (metaXP > 0 && xpAtual >= metaXP)
        {
            LevelUP(metaXP);
        }
    }

    void LevelUP(int metaXP)
    {
        atributos.nivel++;
        xpAtual -= metaXP; // Pega o XP usado e guarda o restante

        // Recalcula os atributos e atualiza a barra de vida
        atributos.CalcularStatus();
        atributos.hpAtual = atributos.hpMaximo;
        atributos.AtualizarBarra();

        Debug.Log($"LEVEL UP! O Heroi alcançou o nivel {atributos.nivel} !");

        // Verifica se o player pode subir mais de um nível seguido
        GanharXP(0);
    }

    int ObterXPProximoNivel()
    {
        int nivelIndex = atributos.nivel - 1;

        // Se ainda estiver dentro da tabela
        if (nivelIndex < xpNecessariaPorNivel.Length)
        {
            return xpNecessariaPorNivel[nivelIndex];
        }
        else
        {
            // Último valor da tabela
            int ultimoValor = xpNecessariaPorNivel[xpNecessariaPorNivel.Length - 1];

            // Quantos níveis já passaram da tabela
            int niveisExtras = nivelIndex - xpNecessariaPorNivel.Length + 1;

            // Escala infinita (ajuste o 1.5f se quiser)
            return Mathf.RoundToInt(ultimoValor * Mathf.Pow(1.5f, niveisExtras));
        }
    }
}