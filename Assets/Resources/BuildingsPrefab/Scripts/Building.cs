using System;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public int health = 100;

    public int maxHealth = 100;

    public int cost = 10;

    public int level = 1;

    public bool isPressed;

    private bool firstChange;

    public static Action OnBuildingHealthChanged;

    private GameObject tempObj;
    
    public virtual void ChangeHealth(int health)
    { 
        if(!firstChange)
        {
            firstChange = true;
            OnBuildingHealthChanged?.Invoke();
        }


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
        if (PlayerProperties.Instance.getOre()>missingHealth)
        {
            PlayerProperties.Instance.ChangeOreAmount(-missingHealth);
            health += missingHealth;
            BuildingInfo.Instance.UpdateHealth(this.gameObject);
        }

    }

    public virtual void Update()
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
            isPressed = true;

            tempObj = BuildingInfo.Instance.gameObject;
            tempObj.SetActive(true);
            BuildingInfo.Instance.AssignHealth(this.gameObject);
            BuildingInfo.Instance.UpdateText(this.gameObject);

        }
    }

    private void OnDestroy()
    {
        isPressed = false;
        tempObj.SetActive(false);
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPressed = false;
            tempObj.SetActive(false);
        }
    }
    public bool isDead()
    {
        return health<=0;
    }

}
