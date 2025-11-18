using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnInterval = 1f;

    public Transform player; // <--- LE JOUEUR À ASSIGNER

    private Transform[] spawnPoints;
    private float timer;

    void Start()
    {
        spawnPoints = new Transform[4];
        spawnPoints[0] = CreateSpawnPoint(new Vector3(-10, 2, -10));
        spawnPoints[1] = CreateSpawnPoint(new Vector3(-10, 2, 10));
        spawnPoints[2] = CreateSpawnPoint(new Vector3(10, 2, -10));
        spawnPoints[3] = CreateSpawnPoint(new Vector3(10, 2, 10));

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
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // ⭐⭐ ASSIGNATION AUTOMATIQUE DU PLAYER ⭐⭐
        Enemy e = newEnemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.player = player;
        }
    }

    private Transform CreateSpawnPoint(Vector3 position)
    {
        GameObject spawnPointObject = new GameObject("SpawnPoint");
        spawnPointObject.transform.position = position;
        spawnPointObject.transform.parent = transform;
        return spawnPointObject.transform;
    }
}
