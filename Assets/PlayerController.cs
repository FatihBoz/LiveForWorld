using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float RotationSpeed = 5f;
    public float BulletSpeed = 5f;
    public GameObject BulletPrefab;
    public Transform ShootPoint;

    private InputManager inputManager;
    private Rigidbody rb;
    private Camera mainCamera;
    private Animator animator;
    private Vector2 moveDirection;

    private void Awake()
    {

        inputManager = GetComponent<InputManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        inputManager.input.Player.Shoot.performed += Shoot;

        CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>();

        if (vcam != null)
        {
            vcam.Follow = transform;
        }

        Cursor.visible = false;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.rotation.x * BulletSpeed,0, bullet.transform.rotation.z * BulletSpeed);
    }

    private void Move()
    {
        moveDirection = inputManager.GetMoveDirection();
        rb.velocity = new Vector3(moveDirection.x * MoveSpeed, 0, moveDirection.y * MoveSpeed);
    }

    private void Rotate()
    {
        Vector2 mousePosition = inputManager.GetMousePosition();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            // Calculate the direction from the character to the target position
            Vector3 direction = targetPosition - transform.position;

            // Ignore the Y component to keep the rotation constrained to the Y-axis
            direction.y = 0;

            // Calculate the rotation needed to look at the target direction
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Apply the rotation to the character
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
        }
    }

    private void Animate()
    {
        if (moveDirection == Vector2.zero)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Animate();
    }

}
