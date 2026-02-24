using UnityEngine;

public class ReturnToPoolAreaer : MonoBehaviour
{
    [Header ("Collider Area")]
    [SerializeField] private ObjectPool objectPool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("ball Left the area");

            objectPool.ReturnToPool(other.gameObject);

            GameManager.GetInstance().SetCurrentNumberOfBalls(GameManager.GetInstance().GetCurrentNumberOfBalls() - 1);

            if (GameManager.GetInstance().GetCurrentNumberOfBalls() > 0)
            {
                objectPool.GetFromPool();
            }

            if (GameManager.GetInstance().GetCurrentNumberOfBalls() <= 0)
            {
                RoundState currentRound = (RoundState)GameManager.GetInstance().GetCurrentState();
                currentRound.FinishRound();
            }

            if (Score.GetInstance().GetScore() >= GameManager.GetInstance().GetRequredScore())
            {
                //TODO: make sure that the player gets the gold for the balls left in the pool
                Gold.GetInstance().AddGold(GameManager.GetInstance().GetCurrentNumberOfBalls());

                objectPool.ReturnEverthingToPool();
                GameManager.GetInstance().SetCurrentNumberOfBalls(0);

                RoundState currentRound = (RoundState)GameManager.GetInstance().GetCurrentState();
                currentRound.FinishRound();
            }
        } 
    }
}
