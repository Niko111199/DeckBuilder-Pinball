using UnityEngine;

public class Gold : MonoBehaviour
{
    public static Gold Instance { get; private set; }

    public int goldAmount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
