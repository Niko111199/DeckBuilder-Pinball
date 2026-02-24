using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera sceneCamera;
    private Vector3 lastPositon;
    [Header("Layer Mask")]
    [SerializeField] private LayerMask PlacementLayerMask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, PlacementLayerMask))
        {
           lastPositon = hit.point;
        }
        return lastPositon;
    }
}
