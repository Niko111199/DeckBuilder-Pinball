using UnityEngine;

public class GoldVisuelliser : MonoBehaviour
{
    [Header("Text Area")]
    [SerializeField] private TMPro.TMP_Text GoldText;

    private void Start()
    {
        Gold.GetInstance().OnGoldAmountChanged += UpdateGoldText;

        UpdateGoldText(Gold.GetInstance().GetGold());
    }

    private void OnDestroy()
    {
        if (Gold.GetInstance() != null)
            Gold.GetInstance().OnGoldAmountChanged -= UpdateGoldText;
    }

    private void UpdateGoldText(int amount)
    {
        GoldText.text = "Gold: " + amount.ToString();
    }
}
