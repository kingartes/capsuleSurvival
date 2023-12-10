using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private enum MovementType
    {
        Screen,
        Local
    }

    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private MovementType movementType = MovementType.Screen;

    private GameInput gameInput;
    private Rigidbody rigidbodyComponent;


    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gameInput = GameInput.Instance;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = CalculateMoveDirection();
        Vector3 lookDirection = GetLookDirection();
        rigidbodyComponent.velocity = moveDirection * speed;

        transform.forward = Vector3.Slerp(transform.forward, lookDirection, Time.deltaTime * rotationSpeed);   
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector2 inputDirection = gameInput.GetMovementNormalized();
        Vector3 movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

        if (movementType == MovementType.Local)
        {
            movementDirection = movementDirection.TransformToBase(transform.forward, transform.up, transform.right).normalized;
        }
        return movementDirection;
    }

    private Vector3 GetLookDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 viewPortPosition = Camera.main.ScreenToViewportPoint(mousePosition);
        Vector3 lookDirection = new Vector3(viewPortPosition.x, 0, viewPortPosition.y);
        Vector3 targetPosition = lookDirection * 2 - new Vector3(1, 0, 1);
        return targetPosition;
    }
}
