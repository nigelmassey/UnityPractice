using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 direction;
    private bool isGrounded = true;
    private Transform groundChecker;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        groundChecker = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");
        var jumpKeyPressed = Input.GetKeyDown(KeyCode.Space);

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        if (isGrounded && jumpKeyPressed)
        {
            velocity.y = 2f;
        }

        direction = new Vector3(horizontalMovement, velocity.y, verticalMovement);
    }

    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
        {
            characterController.Move(direction * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
