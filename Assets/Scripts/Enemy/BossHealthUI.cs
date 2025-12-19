using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public BossHealth bossHealth;
    public Slider slider;
    public Transform boss;
    public Vector3 offset = new Vector3(0, 2f, 0);
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        if (slider != null)
        {
            slider.minValue = 0;
            slider.maxValue = 1;
            slider.value = 1;
            slider.interactable = false;
        }
    }

    private void Update()
    {
        if (bossHealth == null || boss == null || slider == null) return;

        transform.position = boss.position + offset;
        transform.LookAt(transform.position + cam.transform.forward);
        slider.value = bossHealth.GetHealthPercent();
    }
}

