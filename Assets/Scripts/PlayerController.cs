using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 360f;
    private InputSystem_Actions playerInputActions;
    private Vector3 _input;
    private CharacterController characterController;

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
    }

    private void Onable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void Update()
    {
        GatherInput();

        Look();

        Move();
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(_input);

        Quaternion rotation = Quaternion.LookRotation(multipliedMatrix, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * speed * Time.deltaTime;
    }

    private void GatherInput()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();
        _input = new Vector3(input.x, 0, input.y);
    }
}
