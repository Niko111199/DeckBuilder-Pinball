using UnityEngine;

public class MoneyBumper : PlaceableItem
{
    public MoneyBumper()
    {
        itemName = "Money Bumper";
        description = "A bumper that gives extra money when hit.";
        price = 2;
    }

    public override void SetPrefab()
    {
        PrefabManager prefabmanger = Object.FindAnyObjectByType<PrefabManager>(FindObjectsInactive.Include);
        prefab = prefabmanger.GetPrefab(0);
    }
}
