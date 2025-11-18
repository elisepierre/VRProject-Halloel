using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab de l'ennemi à instancier
    public Transform[] spawnPoints; // Points de spawn possibles pour les ennemis
    public float spawnInterval = 5f; // Intervalle de temps entre les spawns

    private float timer;

    // Start est appelé une fois avant la première exécution de Update
    void Start()
    {
        timer = spawnInterval;
    }

    // Update est appelé une fois par frame
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
        if (spawnPoints.Length == 0 || enemyPrefab == null)
        {
            Debug.LogWarning("Aucun point de spawn ou prefab d'ennemi n'est défini.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
