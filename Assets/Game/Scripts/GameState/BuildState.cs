using UnityEngine;

public class BuildState : GameState
{
    private GridLocator[] gridSystem;
    private GameObject itemToPlace;

    public BuildState(GameManager gameManger) : base(gameManger) { }

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

        gridSystem = Object.FindObjectsByType<GridLocator>(FindObjectsInactive.Include,FindObjectsSortMode.None);

        foreach (var item in gridSystem)
        {
            item.gameObject.SetActive(true);
        }
    }

    public override void Exit()
    {
        if (gridSystem == null) return;

        foreach (var item in gridSystem)
        {
            item.gameObject.SetActive(false);
        }

        Debug.Log("Closing Build State");
    }
}
