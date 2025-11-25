using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    public Camera cam;
    public float damage = 20f;
    public float range = 100f;

    public void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Health h = hit.collider.GetComponent<Health>();
            if (h != null)
            {
                h.TakeDamage(damage);
            }
        }
    }
}
