using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GrabItem : MonoBehaviour
{
    [Header("Claw Movement")]
    [SerializeField] private Transform claw;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float downDistance = 2f;

    [Header("Grab Settings")]
    [SerializeField] private float grabRadius = 0.5f;
    [SerializeField] private string grabTag = "Prize";
    [SerializeField] private Vector3 grabOffset = Vector3.zero; 

    [Header("Drop Point")]
    [SerializeField] private Transform dropPoint;

    [Header("Input")]
    [SerializeField] private InputAction grabAction;

    private bool isMoving = false;
    private GameObject grabbedObject = null;

    private bool HasTicket = false;

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

    public bool GetHasTicket()
    {
        return HasTicket;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    void StartGrab()
    {
        if (GameManager.GetInstance().GetCurrentState() is ShopState)
        {
            if (!isMoving && HasTicket)
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
                        grabbedObject.transform.localPosition = Vector3.zero; 
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
        HasTicket = false;
    }

    public void buyTicket(int price)
    {
        if(Gold.GetInstance().GetGold() >= price)
        {
            Gold.GetInstance().RemoveGold(price);
            HasTicket = true;
        }
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
