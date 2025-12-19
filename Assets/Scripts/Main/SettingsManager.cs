using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Références UI")]
    public GameObject settingsPanel;
    public GameObject titleText;
    public GameObject playButton;
    public GameObject settingsButton;

    [Header("Boutons de difficulté")]
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    public void OnSettingsClicked()
    {
        Debug.Log("[Settings] Ouverture du menu paramètres");
        settingsPanel.SetActive(true);
        titleText.SetActive(false);
        playButton.SetActive(false);
        settingsButton.SetActive(false);
    }

    public void OnCloseSettings()
    {
        Debug.Log("[Settings] Fermeture du menu paramètres");
        settingsPanel.SetActive(false);
        titleText.SetActive(true);
        playButton.SetActive(true);
        settingsButton.SetActive(true);
    }

    public void OnEasyClicked()
    {
        Debug.Log("Difficulté sélectionnée : Facile");
        PlayerPrefs.SetString("Difficulty", "Facile");
        PlayerPrefs.Save();
    }

    public void OnMediumClicked()
    {
        Debug.Log("Difficulté sélectionnée : Moyen");
        PlayerPrefs.SetString("Difficulty", "Moyen");
        PlayerPrefs.Save();
    }

    public void OnHardClicked()
    {
        Debug.Log("Difficulté sélectionnée : Difficile");
        PlayerPrefs.SetString("Difficulty", "Difficile");
        PlayerPrefs.Save();
    }
}
