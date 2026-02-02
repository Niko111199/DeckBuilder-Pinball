using UnityEngine;

public class BumperPointAdder : MonoBehaviour
{
    public int pointsToAdd = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Score.Instance.AddScore(pointsToAdd);
        }
    }
}
