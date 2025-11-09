// PlayerInputHandler.cs
using UnityEngine;
using UnityEngine.InputSystem; // Namespace necessário para o novo Input System

// Este script gerencia a leitura de todos os inputs do jogador.
// Ele funciona como uma ponte central: lê os dados do Input System
// e os disponibiliza para outros scripts (Movimento, Combate).
public class PlayerInputHandler : MonoBehaviour
{
    // Propriedade pública para o input de movimento (um vetor de direção, ex: {1, 0}).
    // 'get' permite leitura pública, 'private set' permite escrita apenas por este script.
    public Vector2 MovementInput { get; private set; }

    // Propriedade pública para a posição do mouse (coordenadas de tela, ex: {800, 600}).
    public Vector2 MousePosition { get; private set; }

    // Referência para a classe gerada automaticamente pelo nosso arquivo PlayerInputActions.
    private PlayerInputActions playerInputActions;

    // Awake é chamado antes do primeiro frame, ideal para inicializar referências.
    private void Awake()
    {
        // Cria uma nova instância do nosso controle de input.
        playerInputActions = new PlayerInputActions();
    }

    // OnEnable é chamado quando o objeto se torna ativo.
    // É o local ideal para registrar "eventos" (escutar por inputs).
    private void OnEnable()
    {
        // Ativa o Action Map "Player" que criamos.
        playerInputActions.Player.Enable();

        // Inscreve ('+=') nossos métodos para serem chamados quando as ações ocorrerem.

        // Ação "Move" (WASD)
        playerInputActions.Player.Move.performed += OnMove; // Chamado quando uma tecla é pressionada.
        playerInputActions.Player.Move.canceled += OnMove;  // Chamado quando a tecla é solta (volta para {0,0}).

        // Ação "Look" (Mouse)
        playerInputActions.Player.Look.performed += OnLook; // Chamado a cada frame que o mouse se move.
    }

    // OnDisable é chamado quando o objeto é desativado.
    // É crucial se desinscrever ('-=') dos eventos para evitar erros.
    private void OnDisable()
    {
        // Desativa o Action Map para parar de ouvir inputs.
        playerInputActions.Player.Disable();

        // Remove os nossos métodos da lista de ouvintes do evento.
        playerInputActions.Player.Move.performed -= OnMove;
        playerInputActions.Player.Move.canceled -= OnMove;
        playerInputActions.Player.Look.performed -= OnLook;
    }

    // Este método é chamado automaticamente pela ação 'Move'.
    // O 'context' contém os dados do input (o Vector2 de WASD).
    private void OnMove(InputAction.CallbackContext context)
    {
        // Lê o valor do input (um Vector2) e o armazena na nossa variável.
        MovementInput = context.ReadValue<Vector2>();
    }

    // Este método é chamado automaticamente pela ação 'Look'.
    // O 'context' contém os dados do input (o Vector2 da Posição do Mouse).
    private void OnLook(InputAction.CallbackContext context)
    {
        // Lê o valor do input (um Vector2) e o armazena.
        MousePosition = context.ReadValue<Vector2>();
    }

    // Método público para outros scripts (como o PlayerCombat) verificarem
    // se o botão de tiro foi pressionado *neste frame*.
    public bool IsFireButtonPressed()
    {
        // 'WasPressedThisFrame' é o equivalente do antigo Input.GetKeyDown. 
        return playerInputActions.Player.Fire.WasPressedThisFrame();
    }
}
