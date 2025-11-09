// EnemyPatrol.cs
using UnityEngine;

// Garante que este GameObject também tenha um Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Configurações de Patrulha")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform pontoA;
    [SerializeField] private Transform pontoB;

    // Armazena o alvo atual (pontoA ou pontoB)
    private Transform targetPoint;

    // Referência para o Rigidbody
    private Rigidbody2D rb;

    // Awake é chamado antes de Start e OnEnable
    void Awake()
    {
        // Pega a referência do Rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Garante que a gravidade não afete o inimigo
        rb.gravityScale = 0;
    }

    // OnEnable é chamado sempre que o script é habilitado (pelo EnemyAI).
    void OnEnable()
    {
        // Verificação de segurança
        if (pontoA == null || pontoB == null)
        {
            Debug.LogError("Pontos de patrulha não foram definidos para " + gameObject.name);
            this.enabled = false;
            return;
        }

        // Define o alvo inicial
        targetPoint = pontoA;
    }

    // FixedUpdate é o local correto para lógica de física.
    void FixedUpdate()
    {
        // Verifica se o inimigo chegou perto o suficiente do seu alvo.
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Para o inimigo completamente
            rb.linearVelocity = Vector2.zero;

            // Lógica de troca de alvo
            if (targetPoint == pontoA)
            {
                targetPoint = pontoB;
            }
            else
            {
                targetPoint = pontoA;
            }
        }
        else
        {
            // Se ainda não chegou, continua se movendo em direção ao alvo.

            // 1. Calcula a direção para o alvo
            Vector2 direction = (targetPoint.position - transform.position).normalized;

            // 2. Define a velocidade do Rigidbody
            rb.linearVelocity = direction * moveSpeed;
        }
    }
}