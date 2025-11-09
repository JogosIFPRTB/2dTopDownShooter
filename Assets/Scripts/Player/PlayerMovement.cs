// PlayerMovement.cs
using UnityEngine;

// Garante que o GameObject que tem este script também tenha os componentes Rigidbody2D e PlayerInputHandler.
[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [Tooltip("Controla a velocidade de movimento do jogador.")]
    [SerializeField] private float moveSpeed = 5f;

    // Referências para os componentes necessários.
    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;

    // Start é chamado no primeiro frame em que o script está ativo.
    private void Start()
    {
        // Pega as referências dos componentes no mesmo GameObject.
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // FixedUpdate é chamado em intervalos de tempo fixos.
    // É o local correto para aplicar qualquer lógica de física para garantir consistência. 
    private void FixedUpdate()
    {
        // 1. Pega o input de movimento (ex: {0, 1}) do nosso InputHandler.
        Vector2 movementInput = inputHandler.MovementInput;

        // 2. '.normalized' garante que o vetor tenha comprimento 1.
        // Isso evita que o jogador se mova mais rápido nas diagonais. 
        // 3. Multiplica pela velocidade para obter a velocidade desejada.
        Vector2 desiredVelocity = movementInput.normalized * moveSpeed;

        // 4. Aplica a velocidade diretamente ao Rigidbody2D. 
        rb.linearVelocity = desiredVelocity;
    }
}
