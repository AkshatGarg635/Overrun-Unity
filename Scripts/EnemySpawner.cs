using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnRadius = 15f;
    public float initialInterval = 2f;
    public float minimumInterval = 0.3f;   
    public float speedUpRate = 0.05f;      

    private float currentInterval;
    private float timer;
    public float enemySpeed = 3f;

    public static float SurvivalTime = 0f;  

    void Start()
    {
        currentInterval = initialInterval;
        timer = currentInterval;
        SurvivalTime = 0f;
    }

    void Update()
    {
        SurvivalTime += Time.deltaTime;

        // Shrink spawn interval over time
        currentInterval -= speedUpRate * Time.deltaTime;
        currentInterval = Mathf.Max(currentInterval, minimumInterval);

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = currentInterval;
        }
    }
    void SpawnEnemy()
    {
        if (player == null) return;
        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= 50)
            return;

        float halfAngle = 67.5f;
        float randomAngle = Random.Range(-halfAngle, halfAngle);
        Vector3 forward = player.forward;
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
        Vector3 spawnDir = rotation * forward;
        Vector3 spawnPos = player.position + spawnDir * spawnRadius;

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // Set enemy speed so it increases each wave
        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        if (ai != null)
            ai.speed = enemySpeed;
    }

    public void OnNewWave(int wave)
    {
        currentInterval = Mathf.Max(initialInterval - (wave * 0.15f), 0.05f);
        speedUpRate += 0.02f;

        enemySpeed += 0.2f;
    }
}