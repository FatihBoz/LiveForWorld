using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    public float damage;

    public GameObject TurretBulletPrefab;

    public override void UpgradeBuilding()
    {
        if (level < 3 && PlayerProperties.Instance.getOre() > cost)
        {
            PlayerProperties.Instance.ChangeOreAmount(-cost);

            level++;

            health += 50;

            maxHealth += 50;

            damage += 50;

            Debug.Log("Maden seviyesi yükseltildi! Yeni seviye: " + level);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bomber")) {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        
        }
    }

    void DetectTarget(GameObject targetObject)
    { 

    }

    void TurnPosition()
    {

    }
}
