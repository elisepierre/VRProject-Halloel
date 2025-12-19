using UnityEngine;

public class SleighMover : MonoBehaviour
{
    [Header("Trajectoire")]
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 30f;

    [Header("Cadeau")]
    public GameObject giftPrefab;

    [Header("Effets")]
    public GameObject snowTrailPrefab;

    private float totalDistance;
    private bool giftDropped = false;
    private Vector3 moveDirection;
    private float traveledDistance = 0f;

    private GameObject snowTrailInstance;

    private void Start()
    {
        transform.position = startPoint.position;

        totalDistance = Vector3.Distance(startPoint.position, endPoint.position);

        moveDirection = (endPoint.position - startPoint.position).normalized;

        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        Vector3 offset = -moveDirection * 2f;
        Vector3 spawnPosition = transform.position + offset;

        snowTrailInstance = Instantiate(snowTrailPrefab, spawnPosition, Quaternion.identity);
        snowTrailInstance.transform.rotation = transform.rotation;
        snowTrailInstance.transform.SetParent(transform, true);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position += moveDirection * step;
        traveledDistance += step;

        if (!giftDropped && traveledDistance >= totalDistance / 2f)
        {
            DropGift();
        }

        if (traveledDistance >= totalDistance)
        {
            transform.position = endPoint.position;
            Destroy(gameObject, 2f);
        }
    }

    private void DropGift()
    {
        giftDropped = true;

        GameObject gift = Instantiate(giftPrefab, transform.position, Quaternion.identity);

        Rigidbody rb = gift.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
