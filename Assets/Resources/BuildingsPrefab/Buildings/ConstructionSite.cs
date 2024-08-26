using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    public bool isActive = false;
    public string buildingPrefabName;


    private void Start()
    {
        InputManager.Instance.input.Player.ActivateBuilding.performed += ActivateBuilding;
    }

    private void ActivateBuilding(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("�al��t�r �al��t�");
        GameObject buildingPrefab = Resources.Load<GameObject>("BuildingsPrefabs/" + buildingPrefabName);
        isActive = true;
        Instantiate(buildingPrefab, transform.position, Quaternion.identity);
    }
}
