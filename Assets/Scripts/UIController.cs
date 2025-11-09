// UIController.cs
using UnityEngine;
using TMPro; // Namespace necessário para TextMeshPro

// Este script é o "cérebro" da UI.
// Sua única responsabilidade é se inscrever nos eventos estáticos
// do PlayerStats e atualizar os elementos de texto quando
// esses eventos forem disparados.
public class UIController : MonoBehaviour
{
    [Header("Referências de Texto")]
    [Tooltip("Referência ao texto que exibirá a contagem de vidas.")]
    [SerializeField] private TextMeshProUGUI textoVidas;

    [Tooltip("Referência ao texto que exibirá a contagem de moedas.")]
    [SerializeField] private TextMeshProUGUI textoMoedas;

    // OnEnable é chamado quando o objeto se torna ativo.
    // Local ideal para se inscrever em eventos.
    private void OnEnable()
    {
        // Inscreve ('+=') nossos métodos para serem chamados
        // quando os eventos do PlayerStats ocorrerem.
        PlayerStats.OnVidasChanged += UpdateVidasText;
        PlayerStats.OnMoedasChanged += UpdateMoedasText;
    }

    // OnDisable é chamado quando o objeto é desativado.
    // É crucial se desinscrever ('-=') dos eventos.
    private void OnDisable()
    {
        // Remove nossos métodos da lista de ouvintes.
        PlayerStats.OnVidasChanged -= UpdateVidasText;
        PlayerStats.OnMoedasChanged -= UpdateMoedasText;
    }

    // Este método é chamado automaticamente pelo evento 'OnVidasChanged'.
    private void UpdateVidasText(int novasVidas)
    {
        if (textoVidas != null)
        {
            textoVidas.text = $"Vidas: {novasVidas}";
        }
    }

    // Este método é chamado automaticamente pelo evento 'OnMoedasChanged'.
    private void UpdateMoedasText(int novasMoedas)
    {
        if (textoMoedas != null)
        {
            textoMoedas.text = $"Moedas: {novasMoedas}";
        }
    }
}
