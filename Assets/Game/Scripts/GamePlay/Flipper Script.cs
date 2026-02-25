using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperScript : MonoBehaviour
{
    [Header("flipper Settings")]
    [SerializeField] private float restPostion = 0f;
    [SerializeField] private float pressedPosition = 45f;
    [SerializeField] private float flipperStrength = 10000f;
    [SerializeField] private float flipperDamper = 150f;

    [Header("Controlls")]
    [SerializeField] private InputAction flipperAction;

    private HingeJoint hinge;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
    }

    void OnEnable()
    {
        flipperAction.Enable();
    }

    void OnDisable()
    {
        flipperAction.Disable();
    }

    public float GetflipperStrength()
    {
        return flipperStrength;
    }

    public void SetFlipperStrenght(float value)
    {
        flipperStrength = value;
    }

    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = flipperStrength;
        spring.damper = flipperDamper;

            if (flipperAction.IsPressed() && GameManager.GetInstance().GetCurrentState() is RoundState)
            {
                spring.targetPosition = pressedPosition;
            }
            else
            {
                spring.targetPosition = restPostion;
            }

        hinge.spring = spring;
    }
}
