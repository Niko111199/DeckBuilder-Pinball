using UnityEngine;
using UnityEngine.InputSystem;

public class MoveClaw : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputAction railAction;
    [SerializeField] private InputAction carraigerAction;

    [Header("References")]
    [SerializeField] private Transform rail;
    [SerializeField] private Transform carriage;
    [SerializeField] private GrabItem grabber;

    [Header("Settings")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxDistanceRail = 5.0f;
    [SerializeField] private float maxDistanceCarriage = 5.0f;

    private Vector3 railStartPos;
    private Vector3 carriageStartPos;

    private void Start()
    {
        railStartPos = rail.localPosition;
        carriageStartPos = carriage.localPosition;
    }

    void Update()
    {
        if (GameManager.GetInstance().GetCurrentState() is ShopState)
        {
            if (!grabber.GetIsMoving() && grabber.GetHasTicket())
            {
                float railInput = railAction.ReadValue<float>();
                float carriageInput = carraigerAction.ReadValue<float>();

                MoveRail(railInput);
                MoveCarriage(carriageInput);
            }
        }
    }

    void MoveRail(float input)
    {
        Vector3 pos = rail.localPosition;
        pos.x += input * speed * Time.deltaTime;

        float offset = pos.x - railStartPos.x;
        offset = Mathf.Clamp(offset, -maxDistanceRail, maxDistanceRail);

        pos.x = railStartPos.x + offset;
        rail.localPosition = pos;
    }

    void MoveCarriage(float input)
    {
        Vector3 pos = carriage.localPosition;
        pos.y += input * speed * Time.deltaTime;

        float offset = pos.y - carriageStartPos.y;
        offset = Mathf.Clamp(offset, -maxDistanceCarriage, maxDistanceCarriage);

        pos.y = carriageStartPos.y + offset;
        carriage.localPosition = pos;
    }

    private void OnEnable()
    {
        railAction.Enable();
        carraigerAction.Enable();
    }

    private void OnDisable()
    {
        railAction.Disable();
        carraigerAction.Disable();
    }
}
