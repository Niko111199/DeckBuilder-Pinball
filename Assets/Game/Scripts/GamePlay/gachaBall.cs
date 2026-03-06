using UnityEngine;

public class gachaBall : MonoBehaviour
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [SerializeField] private Renderer ballRenderer;

    private Rarity rarity;

    public Rarity GetRarity() => rarity;

    private void Start()
    {
        rarity = SetRarity();
        SetBallColor(rarity);
    }

    private Rarity SetRarity()
    {
        float randomValue = Random.value; // 0.0f - 1.0f

        if (randomValue < 0.60f) return Rarity.Common;
        if (randomValue < 0.85f) return Rarity.Uncommon;
        if (randomValue < 0.95f) return Rarity.Rare;
        if (randomValue < 0.99f) return Rarity.Epic;

        return Rarity.Legendary;
    }

    private void SetBallColor(Rarity rarity)
    {
        Color color = Color.gray;

        switch (rarity)
        {
            case Rarity.Common: color = Color.red; break;
            case Rarity.Uncommon: color = Color.green; break;
            case Rarity.Rare: color = Color.blue; break;
            case Rarity.Epic: color = new Color(0.6f, 0f, 1f); break;
            case Rarity.Legendary: color = Color.yellow; break;
        }

        if (ballRenderer != null)
            ballRenderer.material.color = color;
    }
}