using UnityEngine;

public class ScoreVisualliser : MonoBehaviour
{
    public TMPro.TMP_Text PlayerScoreText;
    public TMPro.TMP_Text RequiredScoreText;

    //TODO: Optimize by updating only when score changes
    void Update()
    {
        PlayerScoreText.text = "Score: " + Score.Instance.GetScore().ToString();

        RequiredScoreText.text = "Required Score: " + GameManager.Instance.requredScore.ToString();
    }
}
