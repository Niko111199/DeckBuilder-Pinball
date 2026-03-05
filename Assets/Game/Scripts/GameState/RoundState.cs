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
        GameManager.GetInstance().SetRequredScore(SetNeedScore(GameManager.GetInstance().GetCurrentRound()));

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

    //TODO: need to be balance tested
    private int SetNeedScore(int roundNumber)
    {
        switch (roundNumber)
        {
            case 1:
                return 100;
            case 2:
                return 500;
            case 3:
                return 1000;
            case 4:
                return 2500;
            case 5:
                return 5000;
            case 6:
                return 10000;
            case 7:
                return 15000;
            case 8:
                return 25000;
            default: 
                return 0;
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
