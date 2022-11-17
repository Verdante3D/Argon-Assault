using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float xStrafeSpeed = 10f;
    [SerializeField] float yStrafeSpeed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 10f;
    [SerializeField] InputAction movement;
    [SerializeField] InputAction firing;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xStrafe;
    float yStrafe;

    void Start()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
        firing.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        firing.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        xStrafe = movement.ReadValue<Vector2>().x;
        yStrafe = movement.ReadValue<Vector2>().y;

        float xOffset = xStrafe * Time.deltaTime * xStrafeSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yStrafe * Time.deltaTime * yStrafeSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yStrafe * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xStrafe * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (firing.triggered)
        {
            Debug.Log("Firing!");
        }
    }
}
