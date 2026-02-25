using UnityEngine;

public class ScoreVisualliser : MonoBehaviour
{
    [Header("Text Area")]
    [SerializeField]private TMPro.TMP_Text PlayerScoreText;
    [SerializeField]private TMPro.TMP_Text RequiredScoreText;

    private void Start()
    {
        Score.GetInstance().OnScoreChanged += UpdateScoreText;

        UpdateScoreText(Score.GetInstance().GetScore());

        GameManager.GetInstance().OnRoundChanged += UpdateRequiredScoreText;
    }

    private void OnDestroy()
    {
        if (Score.GetInstance() != null)
            Score.GetInstance().OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int amount)
    {
        PlayerScoreText.text = "Score: " + amount.ToString();
    }

    private void UpdateRequiredScoreText(int amount)
    {
        RequiredScoreText.text = "Required Score: " + amount.ToString();
    }
}
