using UnityEngine;
using TMPro;

public class ChallengeUI : MonoBehaviour
{
    public TMP_Text challengeText;
    public void ShowChallenge(string newChallenge)
    {
        if (challengeText != null)
            challengeText.text = "Défi : " + newChallenge;

        gameObject.SetActive(true);
    }

    public void HideChallenge()
    {
        gameObject.SetActive(false);
    }
}
