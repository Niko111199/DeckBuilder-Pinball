using UnityEngine;

public class BallsLeftVisuelliser : MonoBehaviour
{
    [Header("Text Area")]
    [SerializeField] private TMPro.TMP_Text BallCounter;

    private void Start()
    {
        GameManager.GetInstance().OnNumberOfBallsChanged += Updateballcounter;

        Updateballcounter(GameManager.GetInstance().GetCurrentNumberOfBalls());
    }

    private void OnDestroy()
    {
        if (GameManager.GetInstance() != null)
            GameManager.GetInstance().OnNumberOfBallsChanged -= Updateballcounter;
    }

    private void Updateballcounter(int amount)
    {
        BallCounter.text = "Balls Left: " + amount.ToString();
    }
}
