using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperScript : MonoBehaviour
{
    public float restPostion = 0f;
    public float pressedPosition = 45f;
    public float flipperStrength = 10000f;
    public float flipperDamper = 150f;
    HingeJoint hinge;
    public InputAction flipperAction;

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


    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = flipperStrength;
        spring.damper = flipperDamper;

        if (flipperAction.IsPressed())
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
