using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Transform player;

    private PlayerControls playerControls;
    private float rotationX;
    private float rotationY;
    private Vector2 rotationXMinMax = new Vector2(-30, 30);
    private Vector3 currentRotation;
    private float smoothTime = 0.2f;
    private Vector3 smoothVelocity = Vector3.zero;

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
        var look = playerControls.Player.Look.ReadValue<Vector2>();

        rotationX += look.y / 2f;
        rotationY += look.x / 4f;

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        // Apply damping between rotation changes
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;

        transform.position = player.position - transform.forward * 7f;
    }
}
