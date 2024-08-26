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
        Debug.Log("Activatiing buikding");
        if(PlayerProperties.Instance.getOre() > buildingPrefab.GetComponent<Building>().cost){
            Debug.Log("ise girdi");
            PlayerProperties.Instance.ChangeOreAmount(buildingPrefab.GetComponent<Building>().cost);
            isActive = true;
            Destroy(this.gameObject);
            Instantiate(buildingPrefab, transform.position, Quaternion.identity);
        }

    }
}
