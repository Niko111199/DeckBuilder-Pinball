using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestockShopButton : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Button button;
    [SerializeField] TextMeshProUGUI TextArear;
    [SerializeField] ObjectPool objectPool;

    private int RestocksLeft = 2;
    private static RestockShopButton Instance;

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

    private void Start()
    {
        UpdateBottonText();
        button.onClick.AddListener(OnButtonClicked);
    }

    public static RestockShopButton GetInstance()
    {
        return Instance;
    }

    private void OnButtonClicked()
    {
        if (RestocksLeft <= 0)
        {
            return;
        }

        objectPool.RestockPool();
        RestocksLeft--;
        UpdateBottonText();
    }

    private void UpdateBottonText()
    {
        TextArear.text = $"Restock({RestocksLeft})";
    }

    public void IncrementRestocksLeft()
    {
        RestocksLeft++;
        UpdateBottonText();
    }
}
