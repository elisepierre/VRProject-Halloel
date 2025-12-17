using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private GameObject currentEnemy;

    void Update()
    {
        if (currentEnemy == null)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        currentEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        EnemyAI ai = currentEnemy.GetComponent<EnemyAI>();
        if (ai != null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                ai.player = playerObj.transform;
        }
    }
}
