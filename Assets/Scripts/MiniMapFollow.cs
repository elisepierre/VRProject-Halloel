using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 30, 0); // Y = hauteur

    void LateUpdate()
    {
        if (player != null)
            transform.position = player.position + offset;
    }
}
