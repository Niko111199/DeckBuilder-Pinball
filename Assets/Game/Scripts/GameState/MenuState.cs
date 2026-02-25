using UnityEngine;

public class MenuState : GameState
{
    public override void Enter()
    {
        Debug.Log("Entering Menu State");

        CameraHandler.GetInstance().MoveCameraSmooth(CameraHandler.GetInstance().GetMenuCamera().transform, 1f);
    }

    public override void Exit()
    {
        Debug.Log("Exiting Menu State");
    }
}
