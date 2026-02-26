using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class WinPriceCollider : MonoBehaviour
{
    private List<ShopItem> prizes = new List<ShopItem>();
    [Header("Reset Ball")]
    [SerializeField]private ObjectPool PrizePool;
    private bool isGettingPrize;

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

    int GetRarityWeight(ShopItem.Rarity rarity)
    {
        switch (rarity)
        {
            case ShopItem.Rarity.Common:
                return 35;
            case ShopItem.Rarity.Uncommon:
                return 25;
            case ShopItem.Rarity.Rare:
                return 20;
            case ShopItem.Rarity.Epic:
                return 13;
            case ShopItem.Rarity.Legendary:
                return 7;
            default:
                return 0;

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Prize") && !isGettingPrize)
        {
            isGettingPrize = true;

            if (prizes.Count == 0)
            {
                return;
            }

            ShopItem selectedPrize = GetWeightedRandomPrize();
            selectedPrize.BuyItem();
            Debug.Log("Awarded prize: " + selectedPrize.GetItemName() + " Raraty is: " + selectedPrize.GetRarity().ToString());

            WaitForSeconds wait = new WaitForSeconds(2f);
            StartCoroutine(ResetBallAfterDelay(collision.gameObject));
        }
    }

    ShopItem GetWeightedRandomPrize()
    {
        int totalWeight = 0;

        foreach (var prize in prizes)
        {
            totalWeight += GetRarityWeight(prize.GetRarity());
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);

        int currentWeight = 0;

        foreach (var prize in prizes)
        {
            currentWeight += GetRarityWeight(prize.GetRarity());

            if (randomValue < currentWeight)
            {
                return prize;
            }
        }

        return prizes[0];
    }

    public void ResetIsGettingPrize()
    {
        isGettingPrize = false;
    }


    IEnumerator ResetBallAfterDelay(GameObject ball)
    {
        yield return new WaitForSeconds(2f);
        PrizePool.ReturnAndRespawn(ball);
    }
}