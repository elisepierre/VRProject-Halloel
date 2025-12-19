using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ChallengeUI challengeUI;

    private bool tutorialFinished = false;

    void Start()
    {
        if (challengeUI != null)
            challengeUI.HideChallenge();
    }

    void Update()
    {
        if (!tutorialFinished)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FinishTutorial();
            }
        }
    }

    public void FinishTutorial()
    {
        tutorialFinished = true;
        Debug.Log("Tutoriel terminé !");

        if (challengeUI != null)
        {
            challengeUI.ShowChallenge("Premier défi !");
        }
    }
}
