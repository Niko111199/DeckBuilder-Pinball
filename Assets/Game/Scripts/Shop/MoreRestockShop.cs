using UnityEngine;

public class MoreRestockShop : ShopItem
{
    public MoreRestockShop()
    {
        itemName = "More RestockTrys";
        description = "gives a singe Restock of the shop.";
        itemRarity = Rarity.Common;
    }

    public override void BuyItem()
    {
        RestockShopButton.GetInstance().IncrementRestocksLeft(); 
    }
}
