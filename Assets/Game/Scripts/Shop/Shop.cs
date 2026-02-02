using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject buttonPrefab;

    private List<ShopItem> shopItems;

    private void Start()
    {
        shopItems = ShopItemLoader.LoadAllShopItems();
        PopulateShop();
    }

    //TODO: make only 3 random items appear in the shop
    private void PopulateShop()
    {
        foreach (var item in shopItems)
        {
            GameObject newButton = Instantiate(buttonPrefab, contentPanel);
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.itemName} - {item.price}";

            Button button = newButton.GetComponent<Button>();
            button.onClick.AddListener(() => TryBuyItem(item));
        }
    }

    //TODO: Add feedback for successful/unsuccessful purchase
    private void TryBuyItem(ShopItem item)
    {
        if (Gold.Instance.goldAmount >= item.price)
        {
            Gold.Instance.goldAmount -= item.price;
            item.BuyItem();
            Debug.Log($"Bought {item.itemName}!");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
