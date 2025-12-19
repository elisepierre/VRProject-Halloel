using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 4f;
    public float minDistanceFromPlayer = 5f;
    public float maxDistanceFromPlayer = 20f;

    private Transform player;
    private float timer;
    private bool spawningActive = false;

    private GhostTutorial tutorial;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        tutorial = FindFirstObjectByType<GhostTutorial>();
        timer = spawnInterval;

        if (player == null) Debug.LogError("Player introuvable !");
    }

    void Update()
    {
        if (!spawningActive)
        {
            return;
        }

        if (player == null) return;

        if (tutorial != null && tutorial.temporaryMessageActive)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            bool success = SpawnEnemy();
            timer = success ? spawnInterval : 0f;
        }
    }

    public void StartSpawning()
    {
        spawningActive = true;
        timer = spawnInterval;
        Debug.Log("Spawning activé !");
    }

    private bool SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return false;

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

            if (NavMesh.SamplePosition(candidatePos, out NavMeshHit hit, 3f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
                found = true;
            }

            attempts++;
        }

        if (!found)
        {
            Debug.LogWarning("Spawn échoué, retry immédiat");
            return false;
        }

        GameObject enemyObj = Instantiate(prefab, spawnPos, Quaternion.identity);
        Enemy ai = enemyObj.GetComponent<Enemy>();
        if (ai != null) ai.player = player;

        Debug.Log("Enemy spawné à : " + spawnPos);
        return true;
    }

    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
        timer = spawnInterval;
        Debug.Log("Nouvel intervalle de spawn : " + spawnInterval);
    }

    public void StopSpawning()
    {
        spawningActive = false;
        Debug.Log("Spawning désactivé !");
    }
}
