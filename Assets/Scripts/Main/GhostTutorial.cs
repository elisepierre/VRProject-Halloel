using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GhostTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;
    public GameObject[] tutorialImages;

    private string[] messages = new string[]
    {
        "Mon Sauveur, tu es enfin là!",
        "Je me présente, je suis Pink, ton guide dans cette nouvelle aventure.",
        "Depuis Halloween, les citrouilles ont pris vie ici et s'attaque à tout le monde.",
        "Ils me font tellement peur que je me suis caché dans le cimetière...",
        "Aide-moi à ramener la paix en réalisant les défis juste ici.",
        "Ta barre de vie est là, si elle se vide, c'est GAME OVER.",
        "Juste là, tu as une mini map et une flèche qui s'affichera après le tutoriel.",
        "Suis cette flèche et trouve les zones Targets roses qui feront avancer l'histoire.",
        "ZQSD pour te déplacer, clic gauche souris pour tirer et espace pour sauter.",
        "Si tu veux prendre de la vitesse, appui sur MAJ + Z en même temps.",
        "Maintenant que tu as les bases, je te laisse. Bon courage !"
    };

    private int currentMessage = 0;
    public bool tutorialActive = false;
    public bool temporaryMessageActive = false;

    private Queue<(string, GameObject)> tempMessageQueue = new Queue<(string, GameObject)>();

    void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
        HideAllImages();
    }

    void Update()
    {

        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (temporaryMessageActive)
                ShowNextTemporaryMessage();
            else if (tutorialActive)
                NextMessage();
        }
    }

    public void StartTutorial()
    {
        tutorialActive = true;
        currentMessage = 0;
        ShowTutorialMessage();
    }

    private void NextMessage()
    {
        currentMessage++;
        if (currentMessage >= messages.Length)
        {
            tutorialActive = false;
            if (tutorialPanel != null)
                tutorialPanel.SetActive(false);
            HideAllImages();
        }
        else
        {
            ShowTutorialMessage();
        }
    }

    private void ShowTutorialMessage()
    {
        ShowMessage(messages[currentMessage]);
        HideAllImages();
        if (tutorialImages != null && currentMessage < tutorialImages.Length && tutorialImages[currentMessage] != null)
            tutorialImages[currentMessage].SetActive(true);
    }

    public void ShowMessage(string message, GameObject image = null)
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
        if (tutorialText != null)
            tutorialText.text = message;
        HideAllImages();
        if (image != null)
            image.SetActive(true);
        tutorialActive = true;
    }

    public void ShowTemporaryMessage(string message, GameObject image = null)
    {
        tempMessageQueue.Enqueue((message, image));
        if (!temporaryMessageActive)
            ShowNextTemporaryMessage();
    }

    private void ShowNextTemporaryMessage()
    {
        if (tempMessageQueue.Count == 0)
        {
            HideTemporaryMessage();
            return;
        }

        var (msg, img) = tempMessageQueue.Dequeue();
        tutorialPanel.SetActive(true);
        if (tutorialText != null)
            tutorialText.text = msg;
        HideAllImages();
        if (img != null) img.SetActive(true);
        temporaryMessageActive = true;
    }

    private void HideAllImages()
    {
        if (tutorialImages == null) return;
        foreach (GameObject img in tutorialImages)
            if (img != null)
                img.SetActive(false);
    }

    private void HideTemporaryMessage()
    {
        temporaryMessageActive = false;
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
        HideAllImages();
    }

    public void SkipTutorial()
    {
        tutorialActive = false;
        temporaryMessageActive = false;
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
        HideAllImages();
    }
}
