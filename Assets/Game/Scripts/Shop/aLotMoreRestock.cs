using UnityEngine;

public class aLotMoreRestock : ShopItem
{
    public aLotMoreRestock()
    {
        itemName = "a lot More RestockTrys";
        description = "gives a three Restocks of the shop.";
        itemRarity = Rarity.Epic;
    }
    public override void BuyItem()
    {
        for (int i = 0; i < 3; i++)
        {
            RestockShopButton.GetInstance().IncrementRestocksLeft();
        }
    }
}
