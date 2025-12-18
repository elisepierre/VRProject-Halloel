using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShockwaveVisual : MonoBehaviour
{
    [Header("Circle settings")]
    public int segments = 64;
    public float yOffset = 0.01f;
    public Color color = Color.red;
    public float width = 0.05f;

    private LineRenderer lr;
    private float radius = 0f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.loop = true;
        lr.useWorldSpace = false;
        lr.positionCount = segments;
        lr.startWidth = width;
        lr.endWidth = width;
        lr.startColor = color;
        lr.endColor = color;
        lr.enabled = false;
    }

    public void SetVisible(bool visible)
    {
        lr.enabled = visible;
    }

    public void UpdateShockwave(float currentRadius)
    {
        radius = Mathf.Max(0f, currentRadius);
        DrawCircle();
    }

    private void DrawCircle()
    {
        float step = 2f * Mathf.PI / segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = step * i;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, new Vector3(x, yOffset, z));
        }
    }
}
