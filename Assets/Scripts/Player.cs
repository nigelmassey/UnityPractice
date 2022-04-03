using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private Transform Camera;

    private Vector3 moveDirection; 
    private Vector3 velocity;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var move = playerControls.Player.Move.ReadValue<Vector2>();
        var jumpKeyPressed = playerControls.Player.Jump.WasPressedThisFrame();

        //isInAir = Physics.OverlapSphere(GroundCheckObject.position, playerMaskRadius, playerMask).Length == 0;
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = 0f;

        if (characterController.isGrounded && jumpKeyPressed)
        {
            velocity.y = 4f;
        }

        moveDirection = new Vector3(move.x, velocity.y, move.y);
    }

    private void FixedUpdate()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 movementInput = Quaternion.Euler(0, Camera.transform.eulerAngles.y, 0) * moveDirection;
            Vector3 movementDirection = movementInput.normalized;

            characterController.Move(movementDirection * speed * Time.deltaTime);

            movementDirection.y = 0f;
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = desiredRotation;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

}
