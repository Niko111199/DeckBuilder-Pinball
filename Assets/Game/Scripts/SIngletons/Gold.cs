using UnityEngine;

public class Gold : MonoBehaviour
{
    private static Gold Instance;

    [Header("Amount Of Gold")]
    [SerializeField] private int goldAmount = 0;

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
    }

    public void RemoveGold(int amount)
    {
        goldAmount -= amount;
        Debug.Log("Gold removed: " + amount);
    }

    public void ClearGold()
    {
        goldAmount = 0;
        Debug.Log("Gold cleared.");
    }

    public int GetGold()
    {
        return goldAmount;
    }
}
