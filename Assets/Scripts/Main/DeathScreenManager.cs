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
    public float delayBeforeStop = 5f;

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
        deathScreenShown = true;

        menuPrincipalUI.SetActive(false);

        deathScreenUI.SetActive(true);

        StartCoroutine(StopAfterDelay());
    }

    IEnumerator StopAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeStop);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Debug.Log("Application.Quit() called");
        Application.Quit();
        #endif
    }
}