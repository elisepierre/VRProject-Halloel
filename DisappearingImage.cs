using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MenuFadeOut : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Image backgroundImage;
    public TextMeshProUGUI titleText;

    [Header("Bouton Jouer")]
    public Button playButton;
    public TextMeshProUGUI playButtonText;

    [Header("Bouton Paramètres")]
    public Button settingsButton;
    public TextMeshProUGUI settingsButtonText;

    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
    }

    public void OnPlayClicked()
    {
        Debug.Log("Bouton JOUER cliqué !");
        playButton.interactable = false;
        settingsButton.interactable = false;
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsed = 0f;

        Color bgColor = backgroundImage.color;
        Color titleColor = titleText.color;
        Color playTextColor = playButtonText.color;
        Color settingsTextColor = settingsButtonText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            backgroundImage.color = new Color(bgColor.r, bgColor.g, bgColor.b, alpha);
            titleText.color = new Color(titleColor.r, titleColor.g, titleColor.b, alpha);

            playButtonText.color = new Color(playTextColor.r, playTextColor.g, playTextColor.b, alpha);
            settingsButtonText.color = new Color(settingsTextColor.r, settingsTextColor.g, settingsTextColor.b, alpha);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
