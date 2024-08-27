using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    //bool isActive = false;
    bool isPressed = false;


    public GameObject buildingPrefab;


    //public GameObject BuildUI;

    private void Start()
    {
        //InputManager.Instance.input.Player.ActivateBuilding.performed += ActivateBuilding;
        //BuildUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPressed)
        {
            ActivateBuilding();
        }
    }

    private void OnCollisionStay(Collision collision)
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

    //UnityEngine.InputSystem.InputAction.CallbackContext obj
    private void ActivateBuilding()
    {
        Debug.Log("Activatiing buikding");

        if (PlayerProperties.Instance.getOre() > buildingPrefab.GetComponent<Building>().cost){
            PlayerProperties.Instance.ChangeOreAmount(-buildingPrefab.GetComponent<Building>().cost);
            Destroy(this.gameObject);
            Instantiate(buildingPrefab, transform.position, Quaternion.identity);
        }

    }
}
