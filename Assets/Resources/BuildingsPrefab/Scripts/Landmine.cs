using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Building
{
    public float explosionRadius = 5f; // May�n�n patlama yar��ap�
    public LayerMask enemyLayer; // D��manlar�n bulundu�u katman

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isPressed)
        {
            UpgradeBuilding();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Tetiklendi�inde d��man�n var olup olmad���n� kontrol et
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("D��man geldii ");
            Explode();
        }
    }

    void Explode()
    {
        // Patlama noktas�ndaki t�m nesneleri belirlenen yar��apta bul
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            // Her d��man� yok et (veya d��man yok etme kodunu �al��t�r)
            Destroy(enemy.gameObject);
        }

        // May�n� yok et
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Edit�rde patlama yar��ap�n� g�rsel olarak g�stermek i�in
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public override void UpgradeBuilding()
    {
        if (level < 3 && PlayerProperties.Instance.getOre() > cost)
        {
            PlayerProperties.Instance.ChangeOreAmount(-cost);

            level++;

            explosionRadius += 2;

            Debug.Log("May�n seviyesi y�kseltildi! Yeni seviye: " + level);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }
}
