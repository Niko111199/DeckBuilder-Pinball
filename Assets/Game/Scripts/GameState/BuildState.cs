using UnityEngine;

public class BuildState : GameState
{
    private GameObject gridSystem;
    private GameObject itemToPlace;

    public BuildState() : base() { }

    public void SetItemToPlace(GameObject item)
    {
        itemToPlace = item;
    }

    public GameObject GetItemToPlace()
    {
        return itemToPlace;
    }

    public override void Enter()
    {
        Debug.Log("Entering Build State");

        CameraHandler.GetInstance().MoveCameraSmooth(CameraHandler.GetInstance().Getpinballcamera().transform, 1.0f);

        PrefabManager prefabmanger = Object.FindAnyObjectByType<PrefabManager>(FindObjectsInactive.Include);
        gridSystem = prefabmanger.GetPrefab(4);

        gridSystem.SetActive(true);
    }

    public override void Exit()
    {
        if (gridSystem == null) return;

        gridSystem.SetActive(false);

        Debug.Log("Closing Build State");
    }
}
