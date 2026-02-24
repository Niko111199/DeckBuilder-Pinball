using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class WinPriceCollider : MonoBehaviour
{
    private List<ShopItem> prizes = new List<ShopItem>();
    [Header("Reset Ball")]
    [SerializeField]private GameObject ResetBallPoint;

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
            Debug.Log("Awarded prize: " + prizes[randomIndex].GetItemName());

            WaitForSeconds wait = new WaitForSeconds(2f);
            StartCoroutine(ResetBallAfterDelay(collision.gameObject));
        }
    }

    //TODO: make this more efficient by using a pool of balls and resetting them instead of instantiating new ones
    IEnumerator ResetBallAfterDelay(GameObject ball)
    {
        yield return new WaitForSeconds(2f);
        ball.transform.position = ResetBallPoint.transform.position;
    }
}