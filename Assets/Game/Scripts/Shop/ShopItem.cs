using UnityEngine;

public abstract class ShopItem
{
    protected enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    protected string itemName;
    protected Rarity itemRarity;
    protected string description;

    public abstract void BuyItem();

    public string GetItemName()
    {
        return itemName;
    }

    public string GetDescription()
    {
        return description;
    }
}
