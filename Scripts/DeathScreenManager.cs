using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DeathScreenManager : MonoBehaviour
{
    [Header("Référence Player")]
    public GameObject player;

    [Header("Références UI")]
    public GameObject deathScreenUI;
    public Image backgroundImage;
    public TextMeshProUGUI titleText;

    [Header("Paramètres")]
    public float delayBeforeFade = 5f;
    public float fadeDuration = 1.5f;
    public string menuSceneName = "MenuEntrant";

    private bool deathScreenShown = false;

    void Start()
    {
        deathScreenUI.SetActive(false);
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
        deathScreenUI.SetActive(true);
        deathScreenShown = true;
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsed = 0f;

        Color bgColor = backgroundImage.color;
        Color titleColor = titleText.color;

        float startAlphaBg = bgColor.a;
        float startAlphaTitle = titleColor.a;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            float alpha = Mathf.Lerp(startAlphaBg, 0f, t);

            backgroundImage.color = new Color(bgColor.r, bgColor.g, bgColor.b, alpha);
            titleText.color = new Color(titleColor.r, titleColor.g, titleColor.b, alpha);

            yield return null;
        }

        SceneManager.LoadScene(menuSceneName);
    }
}
