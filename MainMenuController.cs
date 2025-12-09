using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene")]
    public string gameSceneName = "SampleScene";

    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(gameSceneName))
            SceneManager.LoadScene(gameSceneName);
        else
            Debug.LogError("Nom de scène non défini dans MainMenu.");
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu...");
        Application.Quit();
    }

    public GameObject optionsPanel;
    public void OpenOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }
    public void CloseOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }
}
