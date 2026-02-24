using UnityEngine;

public class BallsLeftVisuelliser : MonoBehaviour
{
    [Header("Text Area")]
    [SerializeField] private TMPro.TMP_Text BallCounter;

    //TODO: optimize so it only updates when the number of balls changes
    void Update()
    {
        BallCounter.text = "Balls Left: " + GameManager.GetInstance().GetCurrentNumberOfBalls().ToString();
    }
}
