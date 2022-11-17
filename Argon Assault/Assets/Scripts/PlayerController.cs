using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves horizontally based on player input")][SerializeField] float xStrafeSpeed = 10f;
    [Tooltip("How fast ship moves vertically based on player input")][SerializeField] float yStrafeSpeed = 10f;
    [Tooltip("How far player moves horizontally")][SerializeField] float xRange = 10f;
    [Tooltip("How far player moves vertically")][SerializeField] float yRange = 10f;

    [Header("Input system")]
    [Tooltip("Map movement keys")][SerializeField] InputAction movement;
    [Tooltip("Map firing keys")][SerializeField] InputAction firing;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")][SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
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
        if (firing.ReadValue<float>() > 0.5f)
        {
            SetLasersActive(true);
        } else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
