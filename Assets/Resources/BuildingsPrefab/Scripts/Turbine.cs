using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : Building
{

    public float knockbackForce = 5f; // Knockback kuvveti
    public int damageAmount = 10; // Verilecek hasar miktar�

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("D��man t�rbine yakaland� ");
            // Knockback etkisi uygula
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 knockbackDirection = collision.transform.position - transform.position;
                knockbackDirection.y = 0; // Y eksenindeki hareketi s�n�rland�rarak yere yap��may� engeller
                enemyRb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse);
            }

            collision.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
            // Hasar uygula
        }
    }

    public override void UpgradeBuilding()
    {
        if (level < 3 && PlayerProperties.Instance.getOre() > cost)
        {
            PlayerProperties.Instance.ChangeOreAmount(-cost);

            level++;

            knockbackForce += 5f;

            damageAmount += 10;

            Debug.Log("May�n seviyesi y�kseltildi! Yeni seviye: " + level);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isPressed)
        {
            UpgradeBuilding();
        }
    }
}
