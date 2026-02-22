using UnityEngine;
using UnityEngine.InputSystem;

public class MoveClaw : MonoBehaviour
{
    public InputAction railAction;
    public InputAction carraigerAction;

    public Transform rail;
    public Transform carriage;    

    public float speed = 3.0f;
    public float maxDistanceRail = 5.0f;
    public float maxDistanceCarriage = 5.0f;

    private Vector3 railStartPos;
    private Vector3 carriageStartPos;

    public GrabItem grabber;

    private void Start()
    {
        railStartPos = rail.localPosition;
        carriageStartPos = carriage.localPosition;
    }

    void Update()
    {
        if (GameManager.Instance.currentState is ShopState)
        {
            if (!grabber.isMoving && grabber.HasTicket)
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
