using UnityEngine;

public class ReturnToPoolAreaer : MonoBehaviour
{
    [Header ("Collider Area")]
    [SerializeField] private ObjectPool objectPool;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;

        Debug.Log("Ball left the area");

        int ballsLeft = GameManager.GetInstance().GetCurrentNumberOfBalls();


        if (Score.GetInstance().GetScore() >=
            GameManager.GetInstance().GetRequredScore())
        {
            Gold.GetInstance().AddGold(ballsLeft);

            objectPool.ReturnEverthingToPool();
            GameManager.GetInstance().SetCurrentNumberOfBalls(0);

            if (GameManager.GetInstance().GetCurrentState() is RoundState roundState)
            {
                roundState.FinishRound();
            }

            return; 
        }

        objectPool.ReturnToPool(other.gameObject);
        ballsLeft--;

        GameManager.GetInstance().SetCurrentNumberOfBalls(ballsLeft);

        if (ballsLeft > 0)
        {
            objectPool.GetFromPool();
        }
        else
        {
            if (GameManager.GetInstance().GetCurrentState() is RoundState roundState)
            {
                roundState.FinishRound();
            };
        }
    }
}
