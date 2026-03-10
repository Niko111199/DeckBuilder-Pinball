using UnityEngine;

public class Canon : PlaceableItem
{
    public Canon()
    {
        itemName = "Canon";
        description = "A canon that shoots the ball., can be contolled with the same inputs as the flippers";
        itemRarity = Rarity.Epic;
    }
    public override void SetPrefab()
    {
        PrefabManager prefabmanger = Object.FindAnyObjectByType<PrefabManager>(FindObjectsInactive.Include);
        prefab = prefabmanger.GetPrefab(2);
    }
 
   //TODO: make the plunger find the main powerslider, for optimaization
   //TODO: make sure the prefab cant softlock in the corners
}