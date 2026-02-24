using UnityEngine;

public class BumperPointAdder : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private int pointsToAdd = 2;
    private int startingPoints;

    private void Start()
    {
        startingPoints = pointsToAdd;
    }

    public void ResetPoints()
    {
        pointsToAdd = startingPoints;
    }

    public int GetPointsToAdd()
    {
        return pointsToAdd;
    }

    public void SetPointsToAdd(int newPoints)
    {
        pointsToAdd = newPoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Score.GetInstance().AddScore(pointsToAdd);
        }
    }
}
