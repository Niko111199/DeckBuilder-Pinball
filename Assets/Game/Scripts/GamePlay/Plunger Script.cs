using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlungerScript : MonoBehaviour
{
    [Header("powerbar Settings")]
    [SerializeField] protected float power;
    [SerializeField] protected float minPower = 0f;
    [SerializeField] protected float maxPower = 100f;
    [SerializeField] protected float chargeSpeed = 50f;
    [SerializeField] protected Slider powerSlider;

    [Header("Controls")]
    [SerializeField] protected InputAction plungerAction;

    protected List<Rigidbody> ballList = new List<Rigidbody>();

    protected virtual void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        power = minPower;
    }

    //TODO: Optimize Out Update function, maby use an evnet??
    protected virtual void Update()
    {
        powerSlider.gameObject.SetActive(ballList.Count > 0);
        powerSlider.value = power;

        if (ballList.Count > 0)
        {
            if (plungerAction.IsPressed())
            {
                power += Time.deltaTime * chargeSpeed;
                if (power > maxPower)
                    power = maxPower;
            }

            if (plungerAction.WasReleasedThisFrame())
            {
                foreach (var ball in ballList)
                {
                    ball.AddForce(transform.forward * power, ForceMode.Impulse);
                }
                power = minPower;
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (!ballList.Contains(rb))
                ballList.Add(rb);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            ballList.Remove(rb);
            power = minPower;
        }
    }

    protected void OnEnable()
    {
        plungerAction.Enable();
    }

    protected virtual void OnDisable()
    {
        plungerAction.Disable();
    }
}
