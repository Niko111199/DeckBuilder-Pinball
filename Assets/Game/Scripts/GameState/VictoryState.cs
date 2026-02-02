using UnityEngine;

public class VictoryState : GameState
{
    public VictoryState(GameManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("VICTORY!");
        // TODO: show victory screen and options to proceed to Endless mode or go to main menu
    }
}
