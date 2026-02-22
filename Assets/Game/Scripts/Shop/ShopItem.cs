using UnityEngine;

public abstract class ShopItem
{
    public string itemName;
    public int price;
    //TODO: add Raraty
    public string description;

    public abstract void BuyItem();
}
