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
            DontDestroyOnLoad(gameObject); // Oyun boyunca saklanmas�n� sa�la
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public Vector2 GetMoveDirection()
    {

        Debug.Log("gET mOVE �ALO��TI");

        Vector2 moveDirection = input.Player.Move.ReadValue<Vector2>();
        Debug.Log("gET MOUSE �ALO��TI : " + moveDirection);

        return moveDirection.normalized;
    }

    public Vector2 GetMousePosition()
    {

        Vector2 mousePosition = input.Player.MousePosition.ReadValue<Vector2>();
        Debug.Log("gET MOUSE �ALO��TI : " + mousePosition);

        return mousePosition;


    }
}
