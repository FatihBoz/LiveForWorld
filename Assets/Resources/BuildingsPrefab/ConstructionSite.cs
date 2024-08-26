using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    public bool isActive = false;
    public GameObject buildingPrefab;


    private void Start()
    {
        InputManager.Instance.input.Player.ActivateBuilding.performed += ActivateBuilding;
    }

    private void ActivateBuilding(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("çalýþtýr çalýþtý");
        isActive = true;
        Instantiate(buildingPrefab, transform.position, Quaternion.identity);
    }
}
