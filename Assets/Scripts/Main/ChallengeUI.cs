using UnityEngine;
using TMPro;

public class ChallengeUI : MonoBehaviour
{
    public TMP_Text challengeText;

    public void SetChallenge(string newChallenge)
    {
        if (challengeText != null)
            challengeText.text = "Défi : " + newChallenge;
    }
}
