using UnityEngine;

public class GoldVisuelliser : MonoBehaviour
{
    public TMPro.TMP_Text GoldText;

    //TODO: optimize so it only updates when the amount of gold changes
    void Update()
    {
        GoldText.text = "Gold: " + Gold.Instance.GetGold().ToString();
    }
}
