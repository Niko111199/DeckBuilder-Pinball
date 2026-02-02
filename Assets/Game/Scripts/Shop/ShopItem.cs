using UnityEngine;

public abstract class ShopItem
{
    public string itemName;
    public int price;
    //TODO: Add icons
    //public Sprite Icon;
    public string description;

    public abstract void BuyItem();
}
