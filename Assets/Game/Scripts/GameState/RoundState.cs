using UnityEngine;

public class RoundState : GameState
{
    private bool roundFinished;


    public RoundState(GameManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Round started: " + GameManager.Instance.currentRound);
        GameManager.Instance.score.ClereScore();
        GameManager.Instance.currentNumberOfBalls = GameManager.Instance.numberOfBalls;
        GameManager.Instance.requredScore = GameManager.Instance.currentRound * 100;

        roundFinished = false;

        GameManager.Instance.ballPool.GetFromPool();
    }

    public override void Update()
    {
        if (!roundFinished)
            return;

        if (GameManager.Instance.score.GetScore() >= GameManager.Instance.requredScore)
        {
            if (GameManager.Instance.currentRound >= GameManager.Instance.FinalRound)
            {
                GameManager.Instance.ChangeState(new VictoryState(GameManager.Instance));
            }
            else
            {
                GameManager.Instance.ChangeState(new ShopState(GameManager.Instance));
            }
        }
        else
        {
            GameManager.Instance.ChangeState(new LoseState(GameManager.Instance));
        }
    }

    public void FinishRound()
    {
        Gold.Instance.AddGold(2);
        roundFinished = true;
    }

    public override void Exit()
    {
        Debug.Log("Round ended");
    }
}
