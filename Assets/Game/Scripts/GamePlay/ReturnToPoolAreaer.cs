using UnityEngine;

public class ReturnToPoolAreaer : MonoBehaviour
{
    public ObjectPool objectPool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("ball Left the area");

            objectPool.ReturnToPool(other.gameObject);

            GameManager.Instance.currentNumberOfBalls--;

            if (GameManager.Instance.currentNumberOfBalls > 0)
            {
                objectPool.GetFromPool();
            }

            if (GameManager.Instance.currentNumberOfBalls <= 0)
            {
                RoundState currentRound = (RoundState)GameManager.Instance.currentState;
                currentRound.FinishRound();
            }

            if (GameManager.Instance.score.playerScore >= GameManager.Instance.requredScore)
            {
                //TODO: make sure that the player gets the gold for the balls left in the pool
                Gold.Instance.AddGold(GameManager.Instance.currentNumberOfBalls);

                objectPool.ReturnEverthingToPool();
                GameManager.Instance.currentNumberOfBalls = 0;

                RoundState currentRound = (RoundState)GameManager.Instance.currentState;
                currentRound.FinishRound();
            }
        } 
    }
}
