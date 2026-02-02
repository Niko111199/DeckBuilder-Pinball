using UnityEngine;

public class MorePoints : ShopItem
{
    public MorePoints()
    {
        itemName = "More Points";
        description = "Increase 5 points earned per bumper hit.";
        price = 2;
    }

    public override void BuyItem()
    {
        BumperPointAdder[] bumpers = GameObject.FindObjectsByType<BumperPointAdder>(FindObjectsSortMode.None);

        foreach (BumperPointAdder bumper in bumpers)
        {
            bumper.pointsToAdd += 5;
        }

    }
}
