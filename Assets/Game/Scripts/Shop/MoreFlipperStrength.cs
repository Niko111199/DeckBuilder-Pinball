using UnityEngine;

public class MoreFlipperStrength : ShopItem
{
    private float powerToAdd = 5000f;
    private float totalPowerAddet = 0f;

    public MoreFlipperStrength()
    {
        itemName = "More Flipper Strength";
        description = "Increases the strength of the flippers by " + powerToAdd;
        itemRarity = Rarity.Common;
    }

    public override void BuyItem()
    {
        FlipperScript[] flippers =
            Object.FindObjectsByType<FlipperScript>(FindObjectsSortMode.None);

        foreach (FlipperScript flipper in flippers)
        {
            float currentStrength = flipper.GetflipperStrength();
            flipper.SetFlipperStrenght(currentStrength + powerToAdd);

            totalPowerAddet += powerToAdd;
        }

        Debug.Log("Flipper strength increased by " + powerToAdd);
    }
}
