using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : Building
{
    public Slider healthBar;
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
    }
    public override void UpgradeBuilding()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {

    }
    
}
