using UnityEngine;

public class IceBumper : PlaceableItem
{
    public IceBumper()
    {
        itemName = "IceBumper";
        description = "A bumper that gives a ball, that melts over time.";
        itemRarity = Rarity.Epic;
    }

    public override void SetPrefab()
    {
        PrefabManager prefabmanger = Object.FindAnyObjectByType<PrefabManager>(FindObjectsInactive.Include);
        prefab = prefabmanger.GetPrefab(3);
    }
}
