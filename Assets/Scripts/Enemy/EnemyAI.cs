// EnemyAI.cs
using UnityEngine;

// Garante que o inimigo tenha os componentes de comportamento.
[RequireComponent(typeof(EnemyPatrol), typeof(EnemyChase))]
public class EnemyAI : MonoBehaviour
{
    [Header("Configurações de IA")]
    [Tooltip("A distância em que o inimigo detecta e começa a perseguir o jogador.")]
    [SerializeField] private float chaseRange = 5f;

    // Referência para o Transform do jogador para calcular a distância. 
    private Transform player;

    // Referências para os scripts de comportamento que este "cérebro" irá controlar.
    private EnemyPatrol patrolBehaviour;
    private EnemyChase chaseBehaviour;

    private void Start()
    {
        // Encontra o GameObject do jogador na cena usando sua tag. 
        // É crucial que o jogador tenha a tag "Player" configurada no Inspector. 
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Pega as referências dos outros scripts de comportamento no mesmo GameObject.
        patrolBehaviour = GetComponent<EnemyPatrol>();
        chaseBehaviour = GetComponent<EnemyChase>();

        // Garante que o inimigo comece no estado de patrulha.
        SwitchToPatrolState();
    }

    private void Update()
    {
        // Se o jogador não foi encontrado, não faz nada.
        if (player == null) return;

        // Calcula a distância entre o inimigo e o jogador a cada frame. 
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Lógica da Máquina de Estados: 
        // Se o jogador está dentro do alcance de perseguição... 
        if (distanceToPlayer < chaseRange)
        {
            // ...muda para o estado de perseguição. 
            SwitchToChaseState();
        }
        else
        {
            // ...senão, volta ou permanece no estado de patrulha. 
            SwitchToPatrolState();
        }
    }

    // Habilita o comportamento de perseguição e desabilita o de patrulha.
    private void SwitchToChaseState()
    {
        // Verifica se já não está neste estado para evitar chamadas desnecessárias.
        if (!chaseBehaviour.enabled)
        {
            chaseBehaviour.enabled = true;
            patrolBehaviour.enabled = false;
        }
    }

    // Habilita o comportamento de patrulha e desabilita o de perseguição.
    private void SwitchToPatrolState()
    {
        if (!patrolBehaviour.enabled)
        {
            patrolBehaviour.enabled = true;
            chaseBehaviour.enabled = false;
        }
    }
}
