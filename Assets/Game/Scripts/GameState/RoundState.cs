using UnityEngine;

public class RoundState : GameState
{
    private bool roundFinished;

    public RoundState() : base() { }

    public override void Enter()
    {
        CameraHandler.GetInstance().MoveCameraSmooth(CameraHandler.GetInstance().Getpinballcamera().transform , 1.0f);

        Debug.Log("Round started: " + GameManager.GetInstance().GetCurrentRound());
        Score.GetInstance().ClereScore();
        GameManager.GetInstance().SetCurrentNumberOfBalls(GameManager.GetInstance().GetNumberOfBalls());
        GameManager.GetInstance().SetRequredScore(GameManager.GetInstance().GetCurrentRound() * 100);

        roundFinished = false;

        GameManager.GetInstance().GetBallPool().GetFromPool();
    }

    public override void Update()
    {
        if (!roundFinished)
            return;

        if (Score.GetInstance().GetScore() >= GameManager.GetInstance().GetRequredScore())
        {
            if (GameManager.GetInstance().GetCurrentRound() >= GameManager.GetInstance().GetFinalRound())
            {
                GameManager.GetInstance().ChangeState(new VictoryState());
            }
            else
            {
                GameManager.GetInstance().ChangeState(new ShopState());
            }
        }
        else
        {
            GameManager.GetInstance().ChangeState(new LoseState());
        }
    }

    public void FinishRound()
    {
        Gold.GetInstance().AddGold(2);
        roundFinished = true;
    }

    public override void Exit()
    {
        Debug.Log("Round ended");
    }
}
