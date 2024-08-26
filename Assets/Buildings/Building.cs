using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int health = 100;

    public int level = 1;

    bool isBuilded = true;

    public void ChangeHealth(int health)
    { 
        this.health -= health;
    }

    public void UpgradeBuilding()
    {
        if (level < 3)
        {
            level++;
            Debug.Log("Bina seviyesi yükseltildi! Yeni seviye: " + level);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }

}
