using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;

    private Transform player;   // récupéré automatiquement
    private Transform[] spawnPoints;
    private float timer;

    void Start()
    {
        // Trouver le joueur automatiquement
        player = GameObject.FindGameObjectWithTag("Player").transform;

        spawnPoints = new Transform[4];
        spawnPoints[0] = CreateSpawnPoint(new Vector3(-10, 1, -10));
        spawnPoints[1] = CreateSpawnPoint(new Vector3(-10, 1, 10));
        spawnPoints[2] = CreateSpawnPoint(new Vector3(10, 1, -10));
        spawnPoints[3] = CreateSpawnPoint(new Vector3(10, 1, 10));

        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Donner le player à l'ennemi
        Enemy e = newEnemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.player = player;
        }
    }

    private Transform CreateSpawnPoint(Vector3 position)
    {
        GameObject obj = new GameObject("SpawnPoint");
        obj.transform.position = position;
        return obj.transform;
    }
}
