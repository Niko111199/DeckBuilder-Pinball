using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopState : GameState
{
    private GameObject pinballcamera;
    private GameObject ShopCamera;

    public ShopState() : base() { }

    public override void Enter()
    {
        GameManager.GetInstance().SetIsShopOpen(true);
        GameManager.GetInstance().getShopUi().SetActive(true);

        pinballcamera = CameraHandler.GetInstance().Getpinballcamera();
        ShopCamera = CameraHandler.GetInstance().GetShopCamera();

        CameraHandler.GetInstance().MoveCameraSmooth(ShopCamera.transform, 1f);

        Debug.Log("Shop opened");
    }

    public override void Update()
    {
        if (GameManager.GetInstance().GetIsShopOpen() == false)
        {
            GameManager.GetInstance().IncrementCurrentRound();
            GameManager.GetInstance().ChangeState(new RoundState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Shop closed");
    }
}

