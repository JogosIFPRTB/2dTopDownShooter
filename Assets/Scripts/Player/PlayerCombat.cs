// PlayerCombat.cs
using UnityEngine;

// Garante que o jogador tenha o componente de input
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Configurações de Combate")]
    [Tooltip("O Prefab do projétil que será disparado.")]
    [SerializeField] private GameObject projectilePrefab;

    [Tooltip("Um objeto filho que marca a posição exata de onde o projétil deve sair.")]
    [SerializeField] private Transform firePoint;

    [Tooltip("A cadência de tiro. Tempo (em segundos) entre cada disparo.")]
    [SerializeField] private float fireRate = 0.5f; // Ex: 0.5f = 2 tiros por segundo

    [Header("Referências da Lógica de Mira")]
    [Tooltip("A câmera principal para converter a posição do mouse de tela para o mundo.")]
    [SerializeField] private Camera mainCamera;

    private PlayerInputHandler inputHandler;

    // Variável para rastrear quando o próximo disparo será permitido
    private float nextFireTime = 0f;

    private void Start()
    {
        // Obtém a referência ao nosso gerenciador de input.
         inputHandler = GetComponent<PlayerInputHandler>(); 
        
        // Se a câmera não foi arrastada no Inspector, tenta encontrar a MainCamera
         if (mainCamera == null) 
        {
             mainCamera = Camera.main;
        }
    }

    // Update é chamado a cada frame
    void Update()
    {
        // A cada frame, atualizamos a direção da mira.
         HandleAiming();

        // Pergunta ao InputHandler se o botão de tiro foi pressionado
        // E ADICIONA a verificação se o tempo atual é maior que o tempo do próximo disparo
         if (inputHandler.IsFireButtonPressed() && Time.time >= nextFireTime)
        {
            // Se as duas condições forem verdadeiras, atualiza o tempo do próximo disparo
            nextFireTime = Time.time + fireRate;

            // Chama nosso método para atirar.
             Shoot();
        }
    }

    // Método que controla a rotação do firePoint com base no mouse.
    private void HandleAiming()
    {
        // 1. Pega a posição do mouse na tela (em pixels) do InputHandler.
         Vector2 mouseScreenPosition = inputHandler.MousePosition;
        
        // 2. Converte a posição da tela (pixels) para a posição no mundo do jogo.
         Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; // Garante que estamos no plano 2D [cite: 578]

        // 3. Calcula o vetor de direção do ponto de disparo (firePoint) até o mouse.
         Vector2 aimDirection = (Vector2)(mouseWorldPosition - firePoint.position);

        // 4. Calcula o ângulo dessa direção em graus.
         float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // 5. Aplica essa rotação ao transform do firePoint.
         firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Método que contém a lógica de disparo.
    void Shoot()
    {
        // Verifica se as referências do prefab e do ponto de disparo foram configuradas.
         if (projectilePrefab != null && firePoint != null)
        {
            // Cria uma nova instância de um Prefab na cena.
             Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}