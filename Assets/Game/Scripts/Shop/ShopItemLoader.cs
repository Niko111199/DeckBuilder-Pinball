using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

public static class ShopItemLoader
{
    //TODO: make this handle abstract classes like placeable items
    public static List<ShopItem> LoadAllShopItems()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();

        var shopItemInstances = types
            .Where(t => t.IsSubclassOf(typeof(ShopItem)) && !t.IsAbstract)
            .Select(t => (ShopItem)Activator.CreateInstance(t))
            .ToList();

        return shopItemInstances;
    }
}
