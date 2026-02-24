using UnityEngine;

public abstract class ShopItem
{
    protected string itemName;
    //TODO: remove Price, its not used anymore
    protected int price;
    //TODO: add Raraty
    protected string description;

    public abstract void BuyItem();

    public string GetItemName()
    {
        return itemName;
    }

    public int GetPrice()
    {
        return price;
    }

    public string GetDescription()
    {
        return description;
    }
}
