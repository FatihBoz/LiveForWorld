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

    public void ChangeHealth(int health)
    { 
        this.health -= health;
    }

    public abstract void UpgradeBuilding();

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player tespit ettim");
            isPressed = true;
            BuildingInfo.Instance.UpdateText(this.gameObject);
            BuildingInfo.Instance.gameObject.SetActive(true);

        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player çýktý");
            isPressed = false;
            BuildingInfo.Instance.gameObject.SetActive(false);


        }
    }

}
