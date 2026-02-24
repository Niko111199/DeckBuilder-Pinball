using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementSystem : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private float indicatorHeight = 1.0f;

    [Header("Grid")]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject ParantObject;

    [ Header("Input")]
    [SerializeField] private MouseInput Input;

    private GameObject indcator;
    private Dictionary<Vector3Int, GameObject> placedObjects = new Dictionary<Vector3Int, GameObject>();

    public void SetNewIndicator(GameObject indicatorprefab)
    {
        indcator = Instantiate(indicatorprefab);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        Vector3 elevatedMousePos = mousePosition + Vector3.up * indicatorHeight;
        Vector3 elevatedCellPos = grid.CellToWorld(gridPosition) + Vector3.up * indicatorHeight;

        indcator.transform.position = elevatedMousePos;
        cellIndicator.transform.position = elevatedCellPos;

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryPlaceObject(gridPosition);
            Debug.Log($"Placing item at Grid Position: {gridPosition} World Position: {elevatedCellPos}");
        }
    }

    private void TryPlaceObject(Vector3Int gridPosition)
    {
        if (GameManager.GetInstance().GetCurrentState() is not BuildState buildState)
            return;

        if (placedObjects.ContainsKey(gridPosition))
        {
            Debug.Log("Field already occupied! Cannot place another object here.");
            return;
        }

        GameObject prefab = buildState.GetItemToPlace();
        if (prefab == null)
            return;

        Vector3 rayStart = grid.CellToWorld(gridPosition) + Vector3.up * 10f;
        Ray ray = new Ray(rayStart, Vector3.down);

        if (!Physics.Raycast(ray, out RaycastHit hit, 20f))
            return;

        GameObject placedObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, ParantObject.transform);

        Collider col = placedObject.GetComponentInChildren<Collider>();
        if (col == null)
        {
            Debug.LogError("Placed object has no collider!");
            Destroy(placedObject);
            return;
        }

        Vector3 spawnPosition = cellIndicator.transform.position;
        float yOffset = col.bounds.extents.y;
        placedObject.transform.position = spawnPosition + Vector3.up * yOffset;

  
        placedObjects[gridPosition] = placedObject;

        Debug.Log($"Placed object at {spawnPosition} on grid {gridPosition}!");

        Destroy(indcator);
        GameManager.GetInstance().ChangeState(new ShopState());
    }
}
