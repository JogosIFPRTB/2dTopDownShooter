// ProjectileController.cs
using UnityEngine;

// Garante que todo projétil tenha um Rigidbody2D para se mover.
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    [Header("Configurações do Projétil")]
    [Tooltip("Controla a velocidade de movimento do projétil.")]
    [SerializeField] private float speed = 10f;

    [Tooltip("Tempo em segundos antes do projétil ser destruído.")]
    [SerializeField] private float durationTime = 2f;

    // Referência ao componente de física.
    private Rigidbody2D rb;

    void Start()
    {
        // Obtém a referência do Rigidbody2D. 
        rb = GetComponent<Rigidbody2D>();

        // Define a velocidade do projétil.
        // 'transform.right' representa o eixo X local do objeto (a direção "para frente" dele). 
        // Como o projétil já foi instanciado com a rotação correta (apontando para o mouse),
        // seu "transform.right" já é a direção que queremos!
        rb.linearVelocity = transform.right * speed;

        // Destroy o objeto depois do tempo de duração especificado na variavel, por exemplo 2 segundos
        Destroy(gameObject, durationTime);
    }

    // Este método é chamado automaticamente quando o colisor do projétil (marcado como 'Is Trigger')
    // entra em contato com outro colisor. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual colidimos tem a tag "Inimigo".
        if (other.CompareTag("Inimigo"))
        {
            // Tenta obter o componente de vida do inimigo.
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Se o inimigo tiver um script de vida, causamos dano a ele.
                enemyHealth.TakeDamage(1);
            }

            // Destrói o projétil após a colisão. 
            Destroy(gameObject);
        }
        // Se colidir com uma "Parede". 
        else if (other.CompareTag("Parede"))
        {
            // Destrói apenas o projétil. 
            Destroy(gameObject);
        }
    }
}
