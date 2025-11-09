// PlayerInteraction.cs
using UnityEngine;

// Garante que o GameObject do Jogador também tenha o script PlayerStats.
// Se você adicionar este script e o PlayerStats não existir, o Unity o adicionará automaticamente.
[RequireComponent(typeof(PlayerStats))]
public class PlayerInteraction : MonoBehaviour
{
    // Referência privada ao componente de stats do jogador.
    private PlayerStats playerStats;

    // Start é chamado no primeiro frame.
    private void Start()
    {
        // Pega a referência do script PlayerStats que está no mesmo GameObject.
        playerStats = GetComponent<PlayerStats>();
    }

    // Este método é chamado automaticamente pela Unity sempre que o colisor
    // do jogador entra em outro colisor que está marcado como "Is Trigger".
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica a TAG do objeto em que o jogador tocou. 
        // Usar CompareTag é mais rápido e eficiente do que 'other.tag == "Moeda"'.

        if (other.CompareTag("Moeda"))
        {
            // Se for uma moeda:
            // 1. Avisa o PlayerStats para adicionar 1 moeda.
            playerStats.AddMoeda(1);

            // 2. Destrói o objeto da moeda que foi coletado. 
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PocaoVida"))
        {
            // Se for uma poção de vida:
            // 1. Avisa o PlayerStats para curar 1 de vida.
            playerStats.Heal(1);

            // 2. Destrói o objeto da poção que foi coletado. 
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Inimigo"))
        {
            // Se for um inimigo (neste caso, o inimigo também é um trigger):
            // 1. Avisa o PlayerStats para tomar 1 de dano.
            playerStats.TakeDamage(1);

            // 2. Note que NÃO destruímos o inimigo, apenas o jogador toma dano.
            // O inimigo será destruído pelo projétil, que é gerenciado pelo EnemyHealth.
        }
    }
}
