using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControl input { get; private set; }


    void Awake()
    {
        input = new PlayerControl();
        input.Player.Enable();
    }

    public Vector2 GetMoveDirection()
    {
        Vector2 moveDirection = input.Player.Move.ReadValue<Vector2>();

        return moveDirection.normalized;
    }

    public Vector2 GetMousePosition()
    {
        Vector2 mousePosition = input.Player.MousePosition.ReadValue<Vector2>();

        return mousePosition;
    }
}
