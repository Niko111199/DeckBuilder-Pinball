using UnityEngine;

public class AddBumper : PlaceableItem
{
    public AddBumper()
    {
        itemName = "Add Bumper";
        description = "A bumper that adds 10 points when hit.";
        price = 1;
    }

    public override void SetPrefab()
    {
        PrefabManager prefabmanger = Object.FindAnyObjectByType<PrefabManager>(FindObjectsInactive.Include);
        prefab = prefabmanger.GetPrefab(1);
    }
}
