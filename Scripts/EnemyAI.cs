using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float speed = 3f;
    public float maxDistance = 40f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Walk towards player
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Destroy if too far
        if (Vector3.Distance(transform.position, player.position) > maxDistance)
            gameObject.SetActive(false);
    }
}