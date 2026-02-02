using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlungerScript : MonoBehaviour
{
    public float power;
    public float minPower = 0f;
    public float maxPower = 100f;
    public float chargeSpeed = 50f;
    public Slider powerSlider;
    public InputAction plungerAction;


    private List<Rigidbody> ballList = new List<Rigidbody>();

    void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        power = minPower;
    }

    void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (!ballList.Contains(rb))
                ballList.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            ballList.Remove(rb);
            power = minPower;
        }
    }

    private void OnEnable()
    {
        plungerAction.Enable();
    }

    private void OnDisable()
    {
        plungerAction.Disable();
    }
}
