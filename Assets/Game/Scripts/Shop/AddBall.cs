using UnityEngine;

public class AddBall:ShopItem
{
    public AddBall()
    {
        itemName = "Extra Ball";
        price = 2;
        description = "Gives you a Ekstra ball each Round.";
    }

    public override void BuyItem()
    {
        GameManager.GetInstance().SetNumberOfBalls(GameManager.GetInstance().GetNumberOfBalls() + 1);
        Debug.Log("Ball Added");
    }
}
