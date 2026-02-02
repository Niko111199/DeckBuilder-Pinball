using UnityEngine;

public abstract class GameState
{
    protected GameManager gameManger;

    public GameState(GameManager gameManger)
    {
        this.gameManger = gameManger;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
