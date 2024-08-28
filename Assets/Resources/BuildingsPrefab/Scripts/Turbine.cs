using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : Building
{

    public float knockbackForce = 5f; // Knockback kuvveti
    public int damageAmount = 10; // Verilecek hasar miktar�

    void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && health>0)
        {
            Debug.Log("D��man t�rbine yakaland� ");
            // Knockback etkisi uygula
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            CharacterController chController =other.GetComponent<CharacterController>();

            if (enemyRb != null)
            {
                Debug.Log("firlattim");
                Vector3 knockbackDirection = other.transform.position - transform.position;
                knockbackDirection.y = 0; // Y eksenindeki hareketi s�n�rland�rarak yere yap��may� engeller
                enemyRb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse);
                chController.SimpleMove(knockbackDirection.normalized * knockbackForce);
            }
            other.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
            // Hasar uygula
        }
    }

     void OnTriggerStay(UnityEngine.Collider other)
    {
          if (other.gameObject.CompareTag("Player"))
        {
            isPressed = true;

            BuildingInfo.Instance.gameObject.SetActive(true);
            BuildingInfo.Instance.AssignHealth(this.gameObject);
            BuildingInfo.Instance.UpdateText(this.gameObject);

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPressed = false;
            BuildingInfo.Instance.gameObject.SetActive(false);


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

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q) && isPressed)
        {
            UpgradeBuilding();
        }
    }
}
