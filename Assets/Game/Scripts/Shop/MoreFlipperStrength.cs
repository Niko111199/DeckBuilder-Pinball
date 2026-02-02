using UnityEngine;

public class MoreFlipperStrength : ShopItem
{
    public float powerToAdd = 5000f;

    public MoreFlipperStrength()
    {
        itemName = "More Flipper Strength";
        description = "Increases the strength of the flippers by " + powerToAdd;
        price = 1;
    }

    public override void BuyItem()
    {
        FlipperScript[] flippers =
            Object.FindObjectsByType<FlipperScript>(FindObjectsSortMode.None);

        foreach (FlipperScript flipper in flippers)
        {
            flipper.flipperStrength += powerToAdd;
        }

        Debug.Log("Flipper strength increased by " + powerToAdd);
    }
}
