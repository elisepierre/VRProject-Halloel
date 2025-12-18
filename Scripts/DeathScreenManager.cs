using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DeathScreenManager : MonoBehaviour
{
    [Header("Référence Player")]
    public GameObject player;

    [Header("Menus")]
    public GameObject menuPrincipalUI;
    public GameObject deathScreenUI;

    [Header("Death Screen")]
    public Image backgroundImage;
    public TextMeshProUGUI titleText;

    [Header("Paramètres")]
    public float delayBeforeFade = 5f;
    public float fadeDuration = 1.5f;

    private bool deathScreenShown = false;

    void Start()
    {
        menuPrincipalUI.SetActive(false);
        deathScreenUI.SetActive(false);
        SetAlpha(1f);
    }

    void Update()
    {
        if (!deathScreenShown && player == null)
        {
            ShowDeathScreen();
        }
    }

    void ShowDeathScreen()
    {
        deathScreenShown = true;

        menuPrincipalUI.SetActive(false);

        deathScreenUI.SetActive(true);
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f);

        deathScreenUI.SetActive(false);
        menuPrincipalUI.SetActive(true);
    }

    void SetAlpha(float alpha)
    {
        Color bg = backgroundImage.color;
        backgroundImage.color = new Color(bg.r, bg.g, bg.b, alpha);

        Color txt = titleText.color;
        titleText.color = new Color(txt.r, txt.g, txt.b, alpha);
    }
}
