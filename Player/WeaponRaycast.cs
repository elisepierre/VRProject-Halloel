using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class WeaponRaycast : MonoBehaviour
{
    public Camera cam;
    public float damage = 20f;
    public float range = 100f;
    public LineRenderer laserLine;
    public InputActionReference fireAction;

    void Update()
    {
        if (fireAction != null && fireAction.action.triggered)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;

        laserLine.SetPosition(0, origin);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            laserLine.SetPosition(1, hit.point);

            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            laserLine.SetPosition(1, origin + direction * range);
        }

        StartCoroutine(FlashLaser());
    }

    IEnumerator FlashLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.05f);
        laserLine.enabled = false;
    }
}
