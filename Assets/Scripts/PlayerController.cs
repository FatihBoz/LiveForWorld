using Cinemachine;
using System;
using System.Collections;
using Unity.VisualScripting;
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
    public AudioClip WalkSf;


    private SoundEffect sf;
    private Rigidbody rb;
    private Camera mainCamera;
    private Animator animator;
    private Vector2 moveDirection;
    private bool isRunning;
    private bool canPlaySf = true;

    private void Awake()
    {
        sf = GetComponent<SoundEffect>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>();

        if (vcam != null)
        {
            vcam.Follow = transform;
        }
    }

    private void Move()
    {
        moveDirection = InputManager.Instance.GetMoveDirection();
        rb.velocity = new Vector3(moveDirection.x * MoveSpeed, 0, moveDirection.y * MoveSpeed);
    }

    private void Rotate()
    {
        Vector2 mousePosition = InputManager.Instance.GetMousePosition();
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
            isRunning = false;
            animator.SetBool("isRunning", isRunning);
        }
        else
        {
            isRunning = true;
            animator.SetBool("isRunning", isRunning);
            if(canPlaySf)
            {
                StartCoroutine(WalkSoundEffect());
            }
        }
        
    }

    private IEnumerator WalkSoundEffect()
    {
        sf.PlaySoundEffect(WalkSf);
        canPlaySf = false;
        yield return new WaitForSeconds(.35f);
        canPlaySf = true;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Animate();
    }

}
