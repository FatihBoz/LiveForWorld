using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    bool isActive = false;
    bool isPressed = false;


    public GameObject buildingPrefab;


    //public GameObject BuildUI;

    private void Start()
    {
        InputManager.Instance.input.Player.ActivateBuilding.performed += ActivateBuilding;
        //BuildUI.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player tespit ettim");
            isPressed = true;
            //BuildUI.SetActive(true);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player tespit ettim");
            isPressed = false;
            //BuildUI.SetActive(true);

        }
    }

    private void ActivateBuilding(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Activatiing buikding");
        if(PlayerProperties.Instance.getOre() > buildingPrefab.GetComponent<Building>().cost && !isActive && isPressed){
            Debug.Log("ise girdi");
            PlayerProperties.Instance.ChangeOreAmount(-buildingPrefab.GetComponent<Building>().cost);
            isActive = true;
            Destroy(this.gameObject);
            Instantiate(buildingPrefab, transform.position, Quaternion.identity);
        }

    }
}
