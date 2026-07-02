using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    public float speed = 3f;
    public float damage = 10f;
    public float damageCooldown = 1f;

    private float lastDamageTime = -999f;
    private NavMeshAgent agent;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        agent = GetComponent<NavMeshAgent>();
        if (agent != null) agent.speed = speed;
    }

    void Update()
    {
        if (player == null) return;

        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            // Fallback: manual move if no NavMeshAgent
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                lastDamageTime = Time.time;
                PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
                if (health != null) health.TakeDamage(damage);
            }
        }
    }
}