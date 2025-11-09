// EnemyAnimation.cs
using UnityEngine;

// Garante que o inimigo tenha os componentes necess�rios
[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    // Hashes para os par�metros da Blend Tree
    private int moveXHash;
    private int moveYHash;

    // Para armazenar a �ltima dire��o e continuar olhando para l� quando parar
    private Vector2 lastDirection = new Vector2(0, -1); // Come�a olhando para baixo

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Converte as strings dos par�metros em IDs (Hashes)
        moveXHash = Animator.StringToHash("MoveX");
        moveYHash = Animator.StringToHash("MoveY");
    }

    void Update()
    {
        // 1. L� a velocidade atual do Rigidbody2D.
        // A IA (EnemyPatrol ou EnemyChase) est� definindo essa velocidade.
        Vector2 velocity = rb.linearVelocity;

        // 2. Verifica se o inimigo est� se movendo
        if (velocity.magnitude > 0.1f)
        {
            // Se estiver se movendo, normaliza o vetor de velocidade
            // para obter uma dire��o pura (comprimento de 1).
            Vector2 moveDirection = velocity.normalized;

            // Armazena esta como a �ltima dire��o
            lastDirection = moveDirection;

            // Define os par�metros na Blend Tree
            animator.SetFloat(moveXHash, moveDirection.x);
            animator.SetFloat(moveYHash, moveDirection.y);
        }
        else
        {
            // 3. Se o inimigo est� PARADO (velocidade < 0.1)
            // Mantemos os par�metros da �ltima dire��o em que ele estava se movendo.
            // Isso garante que ele fique "parado olhando para a �ltima dire��o".
            animator.SetFloat(moveXHash, lastDirection.x);
            animator.SetFloat(moveYHash, lastDirection.y);
        }
    }
}
