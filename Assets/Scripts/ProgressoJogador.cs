using UnityEngine;
using System.Collections.Generic;

public class ProgressoJogador : MonoBehaviour
{
    public int xpAtual = 0;

    [Header("Tabela de XP")]
    public List<int> xpNecessariaPorNivel = new List<int> { 100, 250, 500, 1000, 5000 };

    private AtributosCombate atributos;

    private void Start()
    {
        atributos = GetComponent<AtributosCombate>();

        if (atributos == null)
        {
            Debug.LogError("AtributosCombate não encontrado!");
            return;
        }

        // Carregar dados globais
        if (DadosGlobais.nivelAtualJogador > 1 || DadosGlobais.xpAtualJogador > 0)
        {
            atributos.nivel = DadosGlobais.nivelAtualJogador;
            xpAtual = DadosGlobais.xpAtualJogador;

            atributos.CalcularStatus();
        }
    }

    public void GanharXP(int quantidade)
    {
        xpAtual += quantidade;
        Debug.Log($"Voce ganhou {quantidade} de XP! Total: {xpAtual}");

        int metaXP = ObterXPProximoNivel();

        // Permite subir múltiplos níveis de uma vez
        while (metaXP > 0 && xpAtual >= metaXP)
        {
            LevelUP(metaXP);
            metaXP = ObterXPProximoNivel();
        }
    }

    void LevelUP(int metaXP)
    {
        atributos.nivel++;
        xpAtual -= metaXP;

        // Recalcula atributos
        atributos.CalcularStatus();
        atributos.hpAtual = atributos.hpMaximo;
        atributos.AtualizarBarra();

        // Salvar progresso
        DadosGlobais.nivelAtualJogador = atributos.nivel;
        DadosGlobais.xpAtualJogador = xpAtual;

        Debug.Log($"LEVEL UP! O Herói alcançou o nível {atributos.nivel}!");
    }

    public int ObterXPProximoNivel()
    {
        int nivelAtual = atributos.nivel;
        int nivelIndex = nivelAtual - 1;

        // Se já existe na lista
        if (nivelIndex < xpNecessariaPorNivel.Count)
        {
            return xpNecessariaPorNivel[nivelIndex];
        }

        // Gerar novos níveis automaticamente
        while (xpNecessariaPorNivel.Count <= nivelIndex)
        {
            int ultimoValor = xpNecessariaPorNivel[xpNecessariaPorNivel.Count - 1];

            // Ajuste da progressão (pode mudar 1.5f)
            int novoValor = Mathf.RoundToInt(ultimoValor * 1.5f);

            xpNecessariaPorNivel.Add(novoValor);

            Debug.Log($"Novo nível gerado automaticamente: {novoValor} XP");
        }

        return xpNecessariaPorNivel[nivelIndex];
    }
}