using UnityEngine;
using TMPro;

public class ChallengeManager : MonoBehaviour
{
    public TextMeshProUGUI challengeText;

    private int currentChallengeIndex = 0;

    private string[] challenges = {
        "Éliminer 3 ennemis",
        "Survivre 30 secondes",
        "Trouver l’arme cachée"
    };

    private int enemiesKilled = 0;
    private float survivalTime = 0f;
    private bool weaponFound = false;

    void Start()
    {
        UpdateChallengeUI();
    }

    void Update()
    {
        if (currentChallengeIndex == 1)
        {
            survivalTime += Time.deltaTime;
            if (survivalTime >= 30f)
                CompleteChallenge();
        }

        if (currentChallengeIndex == 2 && weaponFound)
        {
            CompleteChallenge();
        }
    }

    public void EnemyKilled()
    {
        if (currentChallengeIndex == 0)
        {
            enemiesKilled++;
            if (enemiesKilled >= 3)
                CompleteChallenge();
        }
    }

    public void WeaponCollected()
    {
        weaponFound = true;
    }

    public void CompleteChallenge()
    {
        currentChallengeIndex++;
        if (currentChallengeIndex < challenges.Length)
        {
            UpdateChallengeUI();
        }
        else
        {
            challengeText.text = "Tous les défis sont terminés !";
        }
    }

    private void UpdateChallengeUI()
    {
        challengeText.text = "Défi en cours : " + challenges[currentChallengeIndex];
    }
}
