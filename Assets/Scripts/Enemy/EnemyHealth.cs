// EnemyHealth.cs
using UnityEngine;

// Este script pode ser usado em qualquer inimigo (ou até em objetos destrutíveis).
// Sua única responsabilidade é controlar os pontos de vida.
public class EnemyHealth : MonoBehaviour
{
    [Header("Atributos do Inimigo")]
    [Tooltip("A quantidade inicial de vida do inimigo.")]
    [SerializeField] private int maxHealth = 1; // 

    private int currentHealth;

    private void Start()
    {
        // Inicializa a vida atual com o valor máximo definido.
        currentHealth = maxHealth;
    }

    // Método público que pode ser chamado por outros scripts (como o projétil) para causar dano.
    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida atual.
        currentHealth -= damageAmount;

        // Se a vida for menor ou igual a zero, o inimigo deve ser destruído.
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destrói o GameObject ao qual este script está anexado. 
        }
    }
}
