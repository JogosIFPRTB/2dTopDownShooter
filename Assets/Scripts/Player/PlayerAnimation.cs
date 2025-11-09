// PlayerAnimation.cs
using UnityEngine;

// Garante que o GameObject tenha os componentes necessários.
[RequireComponent(typeof(Animator), typeof(PlayerInputHandler))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerInputHandler inputHandler;

    // Hashes para os parâmetros da Blend Tree.
    private int moveXHash;
    private int moveYHash;
    private int isMovingHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();

        // Converte as strings dos parâmetros em IDs inteiros (Hashes) para otimização.
        moveXHash = Animator.StringToHash("MoveX");
        moveYHash = Animator.StringToHash("MoveY");
        isMovingHash = Animator.StringToHash("isMoving");
    }

    private void Update()
    {
        // 1. Lemos o input de movimento diretamente do nosso InputHandler.
        Vector2 moveInput = inputHandler.MovementInput;

        // 2. Calculamos o estado de movimento UMA VEZ
        bool isMoving = moveInput.magnitude > 0.1f;

        // 3. Definimos o booleano no Animator
        animator.SetBool(isMovingHash, isMoving);

        // 4. Se estivermos nos movendo, atualizamos os floats da direção
        if (isMoving)
        {
            animator.SetFloat(moveXHash, moveInput.x);
            animator.SetFloat(moveYHash, moveInput.y);
        }
        // Se não estivermos (isMoving == false), não atualizamos os floats.
        // Isso faz com que a BlendTree "IdleBlend" continue usando os últimos
        // valores de X e Y, mantendo o personagem olhando para a última direção.
    }
}