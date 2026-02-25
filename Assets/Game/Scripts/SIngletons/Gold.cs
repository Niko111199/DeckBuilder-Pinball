using System;
using UnityEngine;

public class Gold : MonoBehaviour
{
    //TODO: make Gold to tickets and make tickets the currency for the clawmashine
    // maby make gold for a shop to by visuals and stuff like that

    private static Gold Instance;

    [Header("Amount Of Gold")]
    [SerializeField] private int goldAmount = 0;

    public event Action<int> OnGoldAmountChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Gold GetInstance()
    {
        return Instance;
    }

    public void AddGold(int amount)
    {
        goldAmount += amount;
        Debug.Log("Gold added: " + amount);
        OnGoldAmountChanged?.Invoke(goldAmount);
    }

    public void RemoveGold(int amount)
    {
        goldAmount -= amount;
        Debug.Log("Gold removed: " + amount);
        OnGoldAmountChanged?.Invoke(goldAmount);
    }

    public void ClearGold()
    {
        goldAmount = 0;
        Debug.Log("Gold cleared.");
        OnGoldAmountChanged?.Invoke(goldAmount);
    }

    public int GetGold()
    {
        return goldAmount;
    }
}
