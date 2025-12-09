using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    public Camera playerCamera;
    public Transform firePoint;
    public float range = 50f;
    public LineRenderer lineRenderer;
    public float lineDuration = 0.05f;

    private float lineTimer = 0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            lineTimer = lineDuration;
        }

        if (lineTimer > 0)
        {
            lineTimer -= Time.deltaTime;
        }
        else if (lineRenderer.positionCount != 0)
        {
            lineRenderer.positionCount = 0;
        }
    }

    public void Shoot()
    {
        if (firePoint == null || playerCamera == null || lineRenderer == null) return;

        Ray ray = new Ray(firePoint.position, playerCamera.transform.forward);
        RaycastHit hit;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, firePoint.position);

        if (Physics.Raycast(ray, out hit, range))
        {
            lineRenderer.SetPosition(1, hit.point);
            EnemyHealth eh = hit.collider.GetComponentInParent<EnemyHealth>();
            if (eh != null)
                eh.TakeDamage(1f / 3f);
        }
        else
        {
            lineRenderer.SetPosition(1, firePoint.position + playerCamera.transform.forward * range);
        }
    }
}
