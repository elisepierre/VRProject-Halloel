using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    [Header("Références")]
    public GameObject boss;
    public Image endImage;
    public Sprite victorySprite;
    public float fadeDuration = 1.5f;

    private CanvasGroup canvasGroup;
    private bool hasStartedFade = false;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        endImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!hasStartedFade && boss == null)
        {
            StartFadeIn();
        }
        if (boss != null)
        {
            StartFadeIn();
        }
    }

    void StartFadeIn()
    {
        hasStartedFade = true;

        endImage.sprite = victorySprite;
        endImage.gameObject.SetActive(true);

        StartCoroutine(FadeInCoroutine());
    }

    System.Collections.IEnumerator FadeInCoroutine()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
