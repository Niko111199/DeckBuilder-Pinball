using UnityEngine;
using UnityEngine.InputSystem;

public class ShopState : GameState
{
    public ShopState(GameManager manager) : base(manager) { }

    public override void Enter()
    {
        GameManager.Instance.IsShopOpen = true;
        GameManager.Instance.Shop.SetActive(true);
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
        GameManager.Instance.Shop.SetActive(false);
        Debug.Log("Shop closed");
    }
}

