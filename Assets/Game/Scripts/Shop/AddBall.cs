using UnityEngine;

public class AddBall:ShopItem
{
    //TODO: Add icon
    public AddBall()
    {
        itemName = "Extra Ball";
        price = 2;
        description = "Gives you a Ekstra ball each Round.";
    }

    public override void BuyItem()
    {
        GameManager.Instance.numberOfBalls += 1;
        Debug.Log("Ball Added");
    }
}
