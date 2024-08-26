using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    public float damage;

    public GameObject TurretBulletPrefab;

    public float detectionRadius = 10f;

    public override void UpgradeBuilding()
    {
        if (level < 3 && PlayerProperties.Instance.getOre() > cost)
        {
            PlayerProperties.Instance.ChangeOreAmount(-cost);

            level++;

            health += 50;

            maxHealth += 50;

            damage += 50;

            Debug.Log("Maden seviyesi y�kseltildi! Yeni seviye: " + level);
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

    void Update()
    {
        FindNearestEnemy(); // En yak�n d��man� bul

    }

    public GameObject FindNearestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= detectionRadius)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null) { return nearestEnemy; }

        return null;
    }

    void OnDrawGizmosSelected()
    {
        // Alg�lama alan�n� g�rselle�tirmek i�in bir �ember �izer
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


    void TurnPosition(GameObject target)
    {

    }

   
}
