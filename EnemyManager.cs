using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Préfabriqué de l'ennemi
    public Transform[] spawnPoints; // Points de spawn des ennemis
    public float spawnInterval = 5f; // Intervalle entre les spawns

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
        Debug.Log("EnemyManager initialized.");
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        Debug.Log("Enemy spawned.");
    }
}
