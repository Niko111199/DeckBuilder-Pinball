using UnityEngine;

public class ScoreVisualliser : MonoBehaviour
{
    [Header("Text Area")]
    [SerializeField]private TMPro.TMP_Text PlayerScoreText;
    [SerializeField]private TMPro.TMP_Text RequiredScoreText;

    //TODO: Optimize by updating only when score changes
    void Update()
    {
        PlayerScoreText.text = "Score: " + Score.GetInstance().GetScore().ToString();

        RequiredScoreText.text = "Required Score: " + GameManager.GetInstance().GetRequredScore().ToString();
    }
}
