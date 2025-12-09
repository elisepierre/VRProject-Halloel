using UnityEngine;

public class EnemyScoreOnDeath : MonoBehaviour
{
    private void Start()
    {
        Health h = GetComponent<Health>();
        h.OnDeath.AddListener(() => GameManager.instance.AddScore(1));
    }
}