using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
               // ContactPoint contact = collision.contacts[0];
                //Vector3 hitPoint = contact.point;
                Debug.Log("player vuruldu");
                PlayerProperties.Instance.DecreasePlayerHealth(damage);
               // Instantiate(enemy.GetBloodEffect(),hitPoint,Quaternion.identity);
        }
    }
}
