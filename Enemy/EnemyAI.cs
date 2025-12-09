using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float stopDistance = 5f;
    private float originalSpeed;

    private void Start()
    {
        originalSpeed = speed;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Vector3 lookDirection = (player.position - transform.position);
            lookDirection.y = 0;
            if (lookDirection != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    public void SetSpeed(float newSpeed) => speed = newSpeed;
    public void ResetSpeed() => speed = originalSpeed;
}
