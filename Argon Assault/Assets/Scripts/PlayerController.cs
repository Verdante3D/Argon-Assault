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

    void Start()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    void Update()
    {
        float xStrafe = movement.ReadValue<Vector2>().x;
        float yStrafe = movement.ReadValue<Vector2>().y;

        float xOffset = xStrafe * Time.deltaTime * xStrafeSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yStrafe * Time.deltaTime * yStrafeSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

    }
}
