using UnityEngine;

public class VictoryState : GameState
{
    public VictoryState() : base() { }

    public override void Enter()
    {
        Debug.Log("VICTORY!");
        // TODO: show victory screen and options to proceed to Endless mode or go to main menu
    }
}
