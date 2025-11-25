using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
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

            Debug.Log("Points de spawn générés : " + spawnPoints.Length);
        }
        else
        {
            Debug.Log("Points de spawn assignés dans l'inspecteur : " + spawnPoints.Length);
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
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Il n'y a pas de points de spawn disponibles !");
            return;
        }

        int index = Random.Range(0, spawnPoints.Length);

        if (index >= spawnPoints.Length || index < 0)
        {
            Debug.LogError("Index de spawn invalide : " + index);
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);

        if (enemy != null)
        {
            enemy.GetComponent<EnemyAI>().player = player;
            Debug.Log("Ennemi spawn à l'index : " + index);
        }
        else
        {
            Debug.LogError("Impossible de créer un ennemi. Le prefab est peut-être mal assigné !");
        }
    }

    Transform CreateSpawnPoint(Vector3 pos)
    {
        GameObject go = new GameObject("SpawnPoint");
        go.transform.position = pos;
        return go.transform;
    }
}
