using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 50f;
    public int damage = 1;

    // Optionnel : effet de tir
    public ParticleSystem muzzleFlash;

    public void Shoot()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();

        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit : " + hit.collider.name);

            // Si l'objet a un script Enemy
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
