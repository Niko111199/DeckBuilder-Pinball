using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector3 lastPositon;
    public LayerMask PlacementLayerMask;

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
