using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 30, 0);

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + offset;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
