using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GrabItem : MonoBehaviour
{
    //TODO: Add Headers to all varribal fildes, to improve workflow

    [Header("Claw Movement")]
    public Transform claw;
    public float moveSpeed = 3f;
    public float downDistance = 2f;

    [Header("Grab Settings")]
    public float grabRadius = 0.5f;
    public string grabTag = "Prize";
    public Vector3 grabOffset = Vector3.zero; 

    [Header("Drop Point")]
    public Transform dropPoint;

    [Header("Input")]
    public InputAction grabAction;

    public bool isMoving = false;
    private GameObject grabbedObject = null;

    private void OnEnable()
    {
        grabAction.Enable();
        grabAction.performed += ctx => StartGrab();
    }

    private void OnDisable()
    {
        grabAction.performed -= ctx => StartGrab();
        grabAction.Disable();
    }

    void StartGrab()
    {
        if (GameManager.Instance.currentState is ShopState)
        {
            if (!isMoving)
                StartCoroutine(GrabRoutine());
        }
    }

    IEnumerator GrabRoutine()
    {
        isMoving = true;
        grabbedObject = null;

        Vector3 preGrabPosition = claw.position;
        Vector3 downPos = preGrabPosition - new Vector3(0, downDistance, 0) + grabOffset;

        while (Vector3.Distance(claw.position, downPos) > 0.01f)
        {
            claw.position = Vector3.MoveTowards(claw.position, downPos, moveSpeed * Time.deltaTime);
            if (grabbedObject == null)
            {
                Vector3 grabCenter = claw.position + grabOffset;
                Collider[] hits = Physics.OverlapSphere(grabCenter, grabRadius);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag(grabTag))
                    {
                        grabbedObject = hit.gameObject;
                        grabbedObject.transform.SetParent(claw);
                        grabbedObject.transform.localPosition = Vector3.zero; // sikrer det følger claw korrekt
                        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                        if (rb != null) rb.isKinematic = true;
                        break;
                    }
                }
            }

            yield return null;
        }

        while (Vector3.Distance(claw.position, preGrabPosition) > 0.01f)
        {
            claw.position = Vector3.MoveTowards(claw.position, preGrabPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (dropPoint != null)
        {
            Vector3 dropPos = dropPoint.position;
            while (Vector3.Distance(claw.position, dropPos) > 0.01f)
            {
                claw.position = Vector3.MoveTowards(claw.position, dropPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        if (grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
            grabbedObject = null;
        }

        while (Vector3.Distance(claw.position, preGrabPosition) > 0.01f)
        {
            claw.position = Vector3.MoveTowards(claw.position, preGrabPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    private void OnDrawGizmos()
    {
        if (claw != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(claw.position + grabOffset, grabRadius);
        }
    }
}
