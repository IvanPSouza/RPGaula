using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum EstadoBatalha
{
    Preparacao, TurnoJogador, TurnoInimigo, Vitoria, Derrota
}
public class SistemaDeTurnos : MonoBehaviour
{
    public EstadoBatalha estadoAtual;

    public Slider slideHeroi;

    AtributosCombate atributosHeroi;

    private List<AtributosCombate> inimigosVivos = new List<AtributosCombate>();

    private void Start()
    {
        estadoAtual = EstadoBatalha.Preparacao;

        StartCoroutine(ConfigurarBatalha());
    }

    IEnumerator ConfigurarBatalha()
    {
        Debug.Log("Preparando a batalha...");

        //eNCONTRA OS PERSONAGENS DENTRO DA ARENA ULTILIZANDO AS TAGS
        atributosHeroi = GameObject.FindGameObjectWithTag("Player").GetComponent<AtributosCombate>();
        atributosHeroi.minhaBarraDeVida = slideHeroi;
        atributosHeroi.AtualizarBarra();

        yield return new WaitForSeconds(1F);

        GameObject[] objsInimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        foreach (GameObject obj in objsInimigos)
        {
            inimigosVivos.Add(obj.GetComponent<AtributosCombate>());
        }

        IniciarTurnoJogador();
    }

    private void IniciarTurnoJogador()
    {
        Debug.Log("Sua vez. Precione ESPAÇO para ATACAR!");
        estadoAtual = EstadoBatalha.TurnoJogador;
    }

    public void BotaoAtacarFraco()
    {
        if(estadoAtual != EstadoBatalha.TurnoJogador)
        {
            return;
        }

        //Define o alvo do ataque. Sempre o primeiro elemento da lista
        AtributosCombate alvo = inimigosVivos[0];
        alvo.ReceberDano(atributosHeroi.danoBase);

        //Se a vida do inimigo chegou a zero, removemos ele de lista
        if(alvo.hpAtual <= 0)
        {
            inimigosVivos.RemoveAt(0);
        }

        VerificarFimDeTurnoJogador();
    }
    public void BotaoPocao()
    {
        if(estadoAtual != EstadoBatalha.TurnoJogador)
        {
            return;
        }

        //CHAMA A FUNÇÃO DE CURA
        atributosHeroi.ReceberCura(30);

        VerificarFimDeTurnoJogador();
    }
    void VerificarFimDeTurnoJogador()
    {
        //Se a fila inimigos ficou VAZIA
        if(inimigosVivos.Count <= 0)
        {
            estadoAtual = EstadoBatalha.Vitoria;
            StartCoroutine(FinalizarBatalha(true));
        }
        else
        {
            //Ainda há inimigos vivos
            estadoAtual = EstadoBatalha.TurnoInimigo;
            StartCoroutine(TurnoDoInimigo());
        }
    }

    IEnumerator TurnoDoInimigo()
    {
        Debug.Log("Inimigo pensando...");

        //Inimigos atacam o jogador
        Debug.Log("Inimigo ataca o jogador");

        foreach(AtributosCombate inimigo in inimigosVivos)
        {
            yield return new WaitForSeconds(2f);
            atributosHeroi.ReceberDano(inimigo.danoBase);

            //Verifica se o heroi morreu
            if(atributosHeroi.hpAtual <= 0)
            {
                break;
            }
        }

        //Verifica se o combate encerrou
        if (atributosHeroi.hpAtual <= 0)
        {
            estadoAtual = EstadoBatalha.Derrota;
            //Finalizar a batalha
            StartCoroutine(FinalizarBatalha(false));
        }
        else
        {
            IniciarTurnoJogador();
        }
    }

    IEnumerator FinalizarBatalha(bool jogadorVenceu)
    {
        DadosGlobais.hpAtualJogador = atributosHeroi.hpAtual;
        yield return new WaitForSeconds(2f);
        if (jogadorVenceu)
        {
            Debug.Log("Vitória! Voltando para o mundo de exploração");
            //ADICIONA O ID DO INIMIGO AO CEMITERIO(LISTA DE DERROTADOS)
            DadosGlobais.inimigosDerrotados.Add(DadosGlobais.idInimigoEmCombate);
            SceneManager.LoadScene("Mundo");
        }
        else
        {
            Debug.Log("Derrota...");
            SceneManager.LoadScene("GameOver");
        }
    }
}