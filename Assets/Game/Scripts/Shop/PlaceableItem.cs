
using UnityEngine;

public abstract class PlaceableItem : ShopItem
{
    protected GameObject prefab;

    public abstract void SetPrefab();

    public override void BuyItem()
    {
        SetPrefab();

       PlacementSystem Placement = Object.FindAnyObjectByType<PlacementSystem>(FindObjectsInactive.Include);
       Placement.SetNewIndicator(prefab);
       BuildState buildState = new BuildState();
       CameraHandler.GetInstance().MoveCameraSmooth(CameraHandler.GetInstance().GetShopCamera().transform, 1);
       buildState.SetItemToPlace(prefab);
       GameManager.GetInstance().ChangeState(buildState);
    }
}
