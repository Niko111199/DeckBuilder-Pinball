using UnityEngine;

public class BallsLeftVisuelliser : MonoBehaviour
{
    public TMPro.TMP_Text BallCounter;

    //TODO: optimize so it only updates when the number of balls changes
    void Update()
    {
        BallCounter.text = "Balls Left: " + GameManager.Instance.currentNumberOfBalls.ToString();
    }
}
