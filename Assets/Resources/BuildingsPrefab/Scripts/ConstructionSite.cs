using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : Building
{
    public GameObject buildingPrefab;



    public override void UpgradeBuilding()
    {
        throw new NotImplementedException();
    }

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


    //UnityEngine.InputSystem.InputAction.CallbackContext obj
    private void ActivateBuilding()
    {
        Debug.Log("Activatiing buikding");

        if (PlayerProperties.Instance.getOre() > buildingPrefab.GetComponent<Building>().cost){
            PlayerProperties.Instance.ChangeOreAmount(-buildingPrefab.GetComponent<Building>().cost);
            Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z); // Y ekseninde 3 birim yukarý
            //Quaternion rotation = Quaternion.Euler(-90, 0, 0);
            Instantiate(buildingPrefab, position,Quaternion.identity);

            Destroy(this.gameObject);

        }

    }
}
