// EnemyChase.cs
using UnityEngine;

// Garante que este GameObject também tenha um Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [Header("Configurações de Perseguição")]
    [Tooltip("A velocidade do inimigo ao perseguir o jogador.")]
    [SerializeField] private float chaseSpeed = 4f;

    // Referência para o alvo (o jogador).
    private Transform player;

    // Referência para o Rigidbody
    private Rigidbody2D rb;

    void Awake()
    {
        // Pega a referência do Rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Garante que a gravidade não afete o inimigo
        rb.gravityScale = 0;
    }

    // OnEnable é chamado quando este script é ativado.
    void OnEnable()
    {
        // Reencontra o jogador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            // Desabilita a si mesmo se não encontrar o jogador
            this.enabled = false;
        }
    }

    // FixedUpdate é o local correto para lógica de física.
    void FixedUpdate()
    {
        // Se por algum motivo o jogador não existir, para de mover e sai.
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 1. Calcula a direção para o jogador
        Vector2 direction = (player.position - transform.position).normalized;

        // 2. Define a velocidade do Rigidbody para perseguir o jogador
        rb.linearVelocity = direction * chaseSpeed;
    }
}