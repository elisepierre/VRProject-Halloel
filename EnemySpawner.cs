using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab de l'ennemi à instancier
    public float spawnInterval = 1f; // Intervalle de temps entre les spawns

    private Transform[] spawnPoints; // Points de spawn aux extrémités du carré
    private float timer;

    // Start est appelé une fois avant la première exécution de Update
    void Start()
    {
        // Initialisation des points de spawn aux extrémités d'un carré 10x10
        spawnPoints = new Transform[4];
        spawnPoints[0] = CreateSpawnPoint(new Vector3(-10, 2, -10)); // Coin inférieur gauche
        spawnPoints[1] = CreateSpawnPoint(new Vector3(-10, 2, 10));  // Coin supérieur gauche
        spawnPoints[2] = CreateSpawnPoint(new Vector3(10, 2, -10));  // Coin inférieur droit
        spawnPoints[3] = CreateSpawnPoint(new Vector3(10, 2, 10));   // Coin supérieur droit

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

    private Transform CreateSpawnPoint(Vector3 position)
    {
        GameObject spawnPointObject = new GameObject("SpawnPoint");
        spawnPointObject.transform.position = position;
        spawnPointObject.transform.parent = transform; // Optionnel : pour organiser les points sous l'objet parent
        return spawnPointObject.transform;
    }
}
