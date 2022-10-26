using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float xStrafeSpeed = 10f;
    [SerializeField] float yStrafeSpeed = 10f;
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
        float newXPos = transform.localPosition.x + xOffset;

        float yOffset = yStrafe * Time.deltaTime * yStrafeSpeed;
        float newYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);

    }
}
