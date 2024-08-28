using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurProjectile : Projectile
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 hitPoint = contact.point;
                enemy.TakeDamage(damage);
                Instantiate(enemy.GetBloodEffect(),hitPoint,Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }
}
