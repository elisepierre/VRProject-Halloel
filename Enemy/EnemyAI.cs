using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    private Vector3 originalSpeed;

    private void Start()
    {
        originalSpeed = new Vector3(speed, speed, speed);
    }

    private void Update()
    {
        if (!player) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void ResetSpeed()
    {
        speed = originalSpeed.x;
    }
}
