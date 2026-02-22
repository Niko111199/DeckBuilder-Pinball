using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopState : GameState
{
    public ShopState(GameManager manager) : base(manager) { }
    public GameObject pinballcamera;
    public GameObject ShopCamera;
    public Camera mainCamera;

    public override void Enter()
    {
        GameManager.Instance.IsShopOpen = true;
        GameManager.Instance.ShopUi.SetActive(true);

        pinballcamera = GameManager.Instance.pinballcamera;
        ShopCamera = GameManager.Instance.ShopCamera;
        mainCamera = Camera.main;

        GameManager.Instance.MoveCameraSmooth(ShopCamera.transform, 1f);

        Debug.Log("Shop opened");
    }

    public override void Update()
    {
        if (GameManager.Instance.IsShopOpen == false)
        {
            GameManager.Instance.currentRound++;
            GameManager.Instance.ChangeState(new RoundState(GameManager.Instance));
        }
    }

    public override void Exit()
    {
        GameManager.Instance.MoveCameraSmooth(pinballcamera.transform, 1f);

        Debug.Log("Shop closed");
    }
}

