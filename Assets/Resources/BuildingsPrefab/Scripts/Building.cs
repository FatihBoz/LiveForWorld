using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public int health = 100;

    public int maxHealth = 100;

    public int cost = 10;

    public int level = 1;

    public bool isPressed;

    public virtual void ChangeHealth(int health)
    { 
        this.health -= health;
        BuildingInfo.Instance.UpdateHealth(this.gameObject);
        if (this.health<=0)
        {
            this.health=0;
        }
    }

    public void RepairBuilding()
    {
        int missingHealth = maxHealth - health;
        PlayerProperties.Instance.ChangeOreAmount(-missingHealth);
        health += missingHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isPressed)
        {
            RepairBuilding();
        }
    }

    public abstract void UpgradeBuilding();

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player tespit ettim");
            isPressed = true;

            BuildingInfo.Instance.gameObject.SetActive(true);
            BuildingInfo.Instance.AssignHealth(this.gameObject);
            BuildingInfo.Instance.UpdateText(this.gameObject);

        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player ��kt�");
            isPressed = false;
            BuildingInfo.Instance.gameObject.SetActive(false);


        }
    }

}
