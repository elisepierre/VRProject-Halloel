using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MenuFadeOut : MonoBehaviour
{
    [Header("Fade")]
    public float fadeDuration = 1f;
    public Image backgroundImage;
    public TextMeshProUGUI titleText;

    [Header("Bouton Jouer")]
    public Button playButton;
    public TextMeshProUGUI playButtonText;
    public Image playButtonImage;

    [Header("Bouton Paramètres")]
    public Button settingsButton;
    public TextMeshProUGUI settingsButtonText;
    public Image settingsButtonImage;

    [Header("Ghost Tutorial")]
    public GhostTutorial ghostTutorial;

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
        Color playImageColor = playButtonImage.color;
        Color settingsImageColor = settingsButtonImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            backgroundImage.color = new Color(bgColor.r, bgColor.g, bgColor.b, alpha);
            titleText.color = new Color(titleColor.r, titleColor.g, titleColor.b, alpha);

            playButtonText.color = new Color(
                playTextColor.r,
                playTextColor.g,
                playTextColor.b,
                alpha
            );

            settingsButtonText.color = new Color(
                settingsTextColor.r,
                settingsTextColor.g,
                settingsTextColor.b,
                alpha
            );

            playButtonImage.color = new Color(
                playImageColor.r,
                playImageColor.g,
                playImageColor.b,
                alpha
            );

            settingsButtonImage.color = new Color(
                settingsImageColor.r,
                settingsImageColor.g,
                settingsImageColor.b,
                alpha
            );

            yield return null;
        }

        gameObject.SetActive(false);

        if (ghostTutorial != null)
        {
            ghostTutorial.StartTutorial();
        }
        else
        {
            Debug.LogWarning("GhostTutorial n'est pas assigné dans MenuFadeOut !");
        }
    }
}

