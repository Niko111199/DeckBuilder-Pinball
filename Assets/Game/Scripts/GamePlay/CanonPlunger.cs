using UnityEngine;
using UnityEngine.InputSystem;

public class CanonPlunger : PlungerScript
{
    [Header("Canon Rotation Settings")]
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private float returnSpeed = 90f;

    //TODO: bind this to the flipper inputs insted
    [Header("Controls")]
    [SerializeField] private InputAction rotationAction;

    [Header("Reference")]
    [SerializeField] private GameObject container;

    private float currentRotation = 0f;
    private Quaternion startRotation;

    protected override void Start()
    {
        base.Start();

        if (container == null)
        {
            Debug.LogError("Container GameObject is not assigned!");
            return;
        }

        startRotation = container.transform.rotation;

        rotationAction.Enable();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("IceBall"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (!ballList.Contains(rb))
                ballList.Add(rb);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("IceBall"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            ballList.Remove(rb);
            power = minPower;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (container == null) return;

        if (ballList.Count > 0)
        {
            float input = rotationAction.ReadValue<float>();

            if (Mathf.Abs(input) > 0.01f)
            {
                float rotationThisFrame = input * rotationSpeed * Time.deltaTime;

                float newRotation = Mathf.Clamp(currentRotation + rotationThisFrame, -maxRotationAngle, maxRotationAngle);
                rotationThisFrame = newRotation - currentRotation;
                currentRotation = newRotation;

                container.transform.Rotate(0f, rotationThisFrame, 0f, Space.Self);
            }
        }
        else
        {
            if (currentRotation != 0f)
            {
                float rotationThisFrame = Mathf.Sign(-currentRotation) * returnSpeed * Time.deltaTime;

                if (Mathf.Abs(rotationThisFrame) > Mathf.Abs(currentRotation))
                    rotationThisFrame = -currentRotation;

                currentRotation += rotationThisFrame;
                container.transform.Rotate(0f, rotationThisFrame, 0f, Space.Self);
            }
        }
    }
}