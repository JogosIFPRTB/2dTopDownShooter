// PlayerStats.cs
using UnityEngine;
using System; // Necessário para usar 'Action', que é a base dos nossos eventos.

// Este script é o "cérebro" dos dados do jogador.
// Ele armazena vidas, moedas e outros atributos.
// Ele usa eventos 'static' para que outros scripts (como UIController ou GameManager)
// possam se inscrever e reagir às mudanças, sem precisar de uma referência direta.
public class PlayerStats : MonoBehaviour
{
    [Header("Atributos Iniciais")]
    [Tooltip("Quantidade inicial de vidas do jogador.")]
    [SerializeField] private int initialVidas = 3;
    [Tooltip("Quantidade inicial de moedas do jogador.")]
    [SerializeField] private int initialMoedas = 0;

    // Propriedades públicas para que outros scripts possam LER os valores atuais.
    // O 'private set' garante que apenas este script pode MODIFICAR os valores.
    public int Vidas { get; private set; }
    public int Moedas { get; private set; }

    // === EVENTOS PÚBLICOS ===
    // Um evento que é disparado quando a vida muda.
    // Ele envia o novo valor de 'vidas' para todos os "ouvintes".
    public static event Action<int> OnVidasChanged;

    // Um evento que é disparado quando a contagem de moedas muda.
    // Ele envia o novo valor de 'moedas' para todos os "ouvintes".
    public static event Action<int> OnMoedasChanged;

    // Um evento que é disparado quando o jogador morre (vidas <= 0).
    public static event Action OnPlayerDied;

    // Start é chamado no primeiro frame.
    private void Start()
    {
        // Define os valores iniciais.
        Vidas = initialVidas;
        Moedas = initialMoedas;

        // Dispara os eventos uma vez no início para garantir que a UI
        // mostre os valores corretos assim que o jogo começar.
        OnVidasChanged?.Invoke(Vidas);
        OnMoedasChanged?.Invoke(Moedas);
    }

    // --- MÉTODOS PÚBLICOS PARA MODIFICAR OS DADOS ---

    // Método chamado pelo PlayerInteraction quando o jogador toca um inimigo.
    public void TakeDamage(int amount)
    {
        Vidas -= amount; // Reduz a vida 

        // '?' é um "null check". Só chama 'Invoke' se houver algum ouvinte (ex: a UI).
        // Dispara o evento, enviando o novo valor de 'Vidas'.
        OnVidasChanged?.Invoke(Vidas);

        // Verifica a condição de morte.
        if (Vidas <= 0)
        {
            // Dispara o evento de morte. O GameManager estará ouvindo isso.
            OnPlayerDied?.Invoke();

            // (Opcional) Você pode desabilitar os scripts de movimento/combate aqui.
            // Ex: GetComponent<PlayerMovement>().enabled = false;
        }
    }

    // Método chamado pelo PlayerInteraction quando o jogador toca uma poção.
    public void Heal(int amount)
    {
        Vidas += amount; // Aumenta a vida 

        // Dispara o evento, enviando o novo valor de 'Vidas'.
        OnVidasChanged?.Invoke(Vidas);
    }

    // Método chamado pelo PlayerInteraction quando o jogador toca uma moeda.
    public void AddMoeda(int amount)
    {
        Moedas += amount; // Aumenta as moedas 

        // Dispara o evento, enviando o novo valor de 'Moedas'.
        OnMoedasChanged?.Invoke(Moedas);
    }
}
