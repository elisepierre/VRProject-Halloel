using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;  // Référence vers le joueur
    public float speed = 5f;  // Vitesse de déplacement

    void Update()
    {
        if (player == null) return; 

        // Direction vers le joueur
        Vector3 direction = (player.position - transform.position).normalized;

        // Déplacement vers le joueur
        transform.position += direction * speed * Time.deltaTime;
    }
}
