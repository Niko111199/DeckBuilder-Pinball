using UnityEngine;

public abstract class GameState
{
    protected GameManager gameManger;

    public GameState()
    {
        gameManger = GameManager.GetInstance();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
