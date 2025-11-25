using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 4f;
    public Transform[] spawnPoints;

    private float timer;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Si aucun spawnpoint assigné dans l'inspecteur, on en génère 4 automatiquement
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPoints = new Transform[4];
            spawnPoints[0] = CreateSpawnPoint(new Vector3(-10, 1, -10));
            spawnPoints[1] = CreateSpawnPoint(new Vector3(-10, 1, 10));
            spawnPoints[2] = CreateSpawnPoint(new Vector3(10, 1, -10));
            spawnPoints[3] = CreateSpawnPoint(new Vector3(10, 1, 10));
        }

        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        if (ai != null) ai.player = player;

        enemy.GetComponent<EnemyAI>().player = player;
    }

    Transform CreateSpawnPoint(Vector3 pos)
    {
        GameObject go = new GameObject("SpawnPoint");
        go.transform.position = pos;
        return go.transform;
    }
}
