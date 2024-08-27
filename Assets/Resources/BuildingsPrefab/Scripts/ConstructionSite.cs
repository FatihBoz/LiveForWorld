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
        if (buildingPrefab != null)
        {
            print("prefab var");
        }
        if (PlayerProperties.Instance != null)
        {
            print("Instance var");
        }
        if (PlayerProperties.Instance.getOre() > buildingPrefab.GetComponent<Building>().cost){
            if(buildingPrefab == null)
            {
                print("prefab yok");
            }
            if (PlayerProperties.Instance == null)
            {
                print("Instance yok");
            }

            Debug.Log("ise girdi");
            PlayerProperties.Instance.ChangeOreAmount(-buildingPrefab.GetComponent<Building>().cost);
            Destroy(this.gameObject);
            Instantiate(buildingPrefab, transform.position, Quaternion.identity);
        }

    }
}
