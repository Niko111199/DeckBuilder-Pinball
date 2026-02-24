using UnityEngine;

public class Score : MonoBehaviour
{
    private static Score Instance;

    [Header("Player Score")]
    [SerializeField] private int playerScore;

    public static Score GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    public void RemoveScore(int amount)
    {
        playerScore -= amount;
        Debug.Log("Score removed: " + amount + ". Total score: " + playerScore);
    }

    public void ClereScore()
    {
        playerScore = 0;
        Debug.Log("Score cleared. Total score: " + playerScore);
    }

    public int GetScore()
    {
        return playerScore;
    }
}
