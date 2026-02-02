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
        } 
    }
}
