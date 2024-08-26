using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControl input { get; private set; }


    public static InputManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            input = new PlayerControl();
            input.Player.Enable();
            DontDestroyOnLoad(gameObject); // Oyun boyunca saklanmasýný saðla
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public Vector2 GetMoveDirection()
    {

        Debug.Log("gET mOVE ÇALOÝÞTI");

        Vector2 moveDirection = input.Player.Move.ReadValue<Vector2>();
        Debug.Log("gET MOUSE ÇALOÝÞTI : " + moveDirection);

        return moveDirection.normalized;
    }

    public Vector2 GetMousePosition()
    {

        Vector2 mousePosition = input.Player.MousePosition.ReadValue<Vector2>();
        Debug.Log("gET MOUSE ÇALOÝÞTI : " + mousePosition);

        return mousePosition;


    }
}
