using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WinScreenManager : MonoBehaviour
{
    [Header("Référence Player")]
    public GameObject player;

    [Header("Menus")]
    public GameObject menuPrincipalUI;
    public GameObject winScreenUI;

    [Header("Win Screen")]
    public Image backgroundImage;

    [Header("Paramètres")]
    public float delayBeforeStop = 5f;

    private bool winScreenShown = false;

    void Start()
    {
        winScreenUI.SetActive(false);
    }

    void Update()
    {
        if (!winScreenShown && player == null)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        winScreenShown = true;

        menuPrincipalUI.SetActive(false);

        winScreenUI.SetActive(true);

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