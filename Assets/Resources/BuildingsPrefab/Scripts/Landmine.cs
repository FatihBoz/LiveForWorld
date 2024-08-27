using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Building
{
    public float explosionRadius = 5f; // Mayýnýn patlama yarýçapý
    public LayerMask enemyLayer; // Düþmanlarýn bulunduðu katman

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isPressed)
        {
            UpgradeBuilding();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Tetiklendiðinde düþmanýn var olup olmadýðýný kontrol et
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Düþman geldii ");
            Explode();
        }
    }

    void Explode()
    {
        // Patlama noktasýndaki tüm nesneleri belirlenen yarýçapta bul
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            // Her düþmaný yok et (veya düþman yok etme kodunu çalýþtýr)
            Destroy(enemy.gameObject);
        }

        // Mayýný yok et
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Editörde patlama yarýçapýný görsel olarak göstermek için
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

            Debug.Log("Mayýn seviyesi yükseltildi! Yeni seviye: " + level);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }
}
