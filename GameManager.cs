using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score = " + score);
    }

    public int GetScore()
    {
        return score;
    }
}