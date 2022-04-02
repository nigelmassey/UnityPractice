using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float playerMaskRadius = 0.07f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform GroundCheckObject;

    private Vector3 direction;
    private bool isInAir = false;    
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        var jumpKeyPressed = Input.GetButtonDown("Jump");

        isInAir = Physics.OverlapSphere(GroundCheckObject.position, playerMaskRadius, playerMask).Length == 0;
        if (!isInAir && velocity.y < 0)
            velocity.y = 0f;

        if (!isInAir && jumpKeyPressed)
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
