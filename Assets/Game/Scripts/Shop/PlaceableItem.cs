
using UnityEngine;

public abstract class PlaceableItem : ShopItem
{
    public GameObject prefab;

    public abstract void SetPrefab();

    public override void BuyItem()
    {
        SetPrefab();

       PlacementSystem Placement = Object.FindAnyObjectByType<PlacementSystem>(FindObjectsInactive.Include);
       Placement.SetNewIndicator(prefab);
        BuildState buildState = new BuildState(GameManager.Instance);
       buildState.SetItemToPlace(prefab);
       GameManager.Instance.ChangeState(buildState);
       GameManager.Instance.Shop.SetActive(false); 
    }
}
