using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : Building
{
    public Slider healthBar;
    public static Action OnSpaceshipCollapsed;
    public void Start()
    {
        health=maxHealth;
        healthBar.maxValue=maxHealth;
        healthBar.value=health;
    }
    public override void ChangeHealth(int health)
    {
        base.ChangeHealth(health);
        healthBar.value=this.health;
        if (this.health <= 0)
        {
            OnSpaceshipCollapsed?.Invoke();
        }
    }
    public override void UpgradeBuilding()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {

    }
    
}
