using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WinPriceCollider : MonoBehaviour
{
    private List<ShopItem> prizes = new List<ShopItem>();

    private void Start()
    {
        LoadAllShopItems();
    }

    void LoadAllShopItems()
    {
        var shopItemType = typeof(ShopItem);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => shopItemType.IsAssignableFrom(t) &&
                        !t.IsAbstract);

        foreach (var type in types)
        {
            ShopItem item = (ShopItem)Activator.CreateInstance(type);
            prizes.Add(item);
        }

        Debug.Log("Loaded " + prizes.Count + " shop items.");
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Prize"))
        {
            if (prizes.Count == 0)
            {
                return;
            }

            int randomIndex = UnityEngine.Random.Range(0, prizes.Count);
            prizes[randomIndex].BuyItem();
            Debug.Log("Awarded prize: " + prizes[randomIndex].itemName);
        }
    }
}