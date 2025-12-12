using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs; // Tableau de prefabs

    [Header("Spawn Settings")]
    public float spawnInterval = 4f;
    public float minDistanceFromPlayer = 5f;
    public float maxDistanceFromPlayer = 20f;

    private float timer;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) Debug.LogError("Player introuvable !");
        timer = spawnInterval;
    }

    private void Update()
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
        if (enemyPrefabs == null || enemyPrefabs.Length == 0 || player == null) return;

        // Choisir un prefab aléatoire
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector3 spawnPos = Vector3.zero;
        bool found = false;
        int attempts = 0;

        while (!found && attempts < 20)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
            Vector3 candidatePos = player.position + offset;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(candidatePos, out hit, 3f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
                found = true;
            }

            attempts++;
        }

        if (!found)
        {
            Debug.LogWarning("Impossible de trouver un spawn valide sur le NavMesh !");
            return;
        }

        GameObject enemyObj = Instantiate(prefab, spawnPos, Quaternion.identity);

        Enemy ai = enemyObj.GetComponent<Enemy>();
        if (ai != null) ai.player = player;

        Debug.Log("Enemy spawned on NavMesh at: " + spawnPos + " using prefab: " + prefab.name);
    }
}
