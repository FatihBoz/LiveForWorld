using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public int health = 100;

    public int maxHealth = 100;

    public int cost = 10;

    public int level = 1;


    public void ChangeHealth(int health)
    { 
        this.health -= health;
    }

    public abstract void UpgradeBuilding();

}
