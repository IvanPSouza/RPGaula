using UnityEngine;

public class TesteInventario : MonoBehaviour
{
    public SistemaInventario sistemaInventario;

    // Campos para arrastar os ScriptableObjects que criamos
    public DadosItem espada;
    public DadosItem pocao;

    void Start()
    {
        Debug.Log("--- INICIANDO TESTE DO INVENTÁRIO ---");

        // Teste 1: Adicionar item único
        sistemaInventario.AdicionarItem(espada, 1);

        // Teste 2: Adicionar item empilhável
        sistemaInventario.AdicionarItem(pocao, 5);

        // Teste 1: Adicionar item único
        sistemaInventario.AdicionarItem(espada, 1);

        // Teste 3: Adicionar MAIS do mesmo item empilhável (Deve somar no slot anterior)
        sistemaInventario.AdicionarItem(pocao, 3);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            sistemaInventario.AdicionarItem(pocao, -1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            sistemaInventario.AdicionarItem(espada, -1);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            sistemaInventario.AdicionarItem(pocao, 1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            sistemaInventario.AdicionarItem(espada, 1);
        }
    }
}