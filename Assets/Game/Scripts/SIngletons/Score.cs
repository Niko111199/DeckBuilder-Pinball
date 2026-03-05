using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static Score Instance;

    [Header("Player Score")]
    [SerializeField] private int playerScore;
    private int hitMultiplier = 1;

    public event Action<int> OnScoreChanged;

    public static Score GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        Debug.Log("Score added: " + amount + ". Total score: " + playerScore);
        OnScoreChanged?.Invoke(playerScore);
    }

    public void RemoveScore(int amount)
    {
        playerScore -= amount;
        Debug.Log("Score removed: " + amount + ". Total score: " + playerScore);
        OnScoreChanged?.Invoke(playerScore);
    }

    public void ClereScore()
    {
        playerScore = 0;
        Debug.Log("Score cleared. Total score: " + playerScore);
        OnScoreChanged?.Invoke(playerScore);
    }

    public int GetHitMultiplier()
    {
        return hitMultiplier;
    }

    public void IncrementHitMultiplier()
    {
        hitMultiplier = Mathf.Min(hitMultiplier + 1, 6);
    }

    public void ResetHitMultiplier()
    {
        hitMultiplier = 1;

        Debug.Log("Hitmultiplayer is reset");
    }

    public int GetScore()
    {
        return playerScore;
    }
}
