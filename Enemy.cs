using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Référence au joueur

    public float speed = 5f; // Vitesse de déplacement de l'ennemi

    // Update est appelé une fois par frame
    void Update()
    {
        if (player != null)
        {
            // Calculer la direction vers le joueur
            Vector3 direction = (player.position - transform.position).normalized;

            // Déplacer l'ennemi vers le joueur
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
