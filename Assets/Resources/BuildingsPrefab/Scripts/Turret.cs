using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    public float damage;

    public GameObject TurretBulletPrefab;

    public Transform FirePoint;

    public Transform FirePoint2;

    public float detectionRadius = 10f;

    public GameObject TurretLvl2;

    GameObject currentTarget;

    public GameObject TurretHead;

    public Animator TurretHeadAnimator;

    private float fireCountdown = 0f; 
    public float fireRate = 1f;
    float bulletDamage = 10;

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

            Instantiate(TurretLvl2);
            Destroy(this);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if(collision.gameObject.CompareTag("Bomber")) {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        
        }
        */
    }

    void Update()
    {
        if(currentTarget == null)
        {
            TurretHeadAnimator.SetBool("FoundEnemy", false);

            FindNearestEnemy();

           
        }
        else
        {
            TurretHeadAnimator.SetBool("FoundEnemy", true);

            RotateHead();

            // Ateþ etme süresi dolduysa, ateþ et
            if (fireCountdown <= 0f)
            {
                Fire();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UpgradeBuilding();
        }

    }

    public void FindNearestEnemy()
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
        if(nearestEnemy != null) { Debug.Log("Found Enemy"); currentTarget = nearestEnemy; }

    }

    void OnDrawGizmosSelected()
    {
        // Algýlama alanýný görselleþtirmek için bir çember çizer
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


    void RotateHead()
    {
        if (currentTarget == null)
        {
            Debug.LogWarning("No target set for the turret.");
            return;
        }

        // Hedefin yönünü hesapla
        Vector3 direction = currentTarget.transform.position - TurretHead.transform.position;

        // Yalnýzca Y ekseni üzerinde döndürmek için Y düzlemine projekte et
        direction.x = 270f;

        if (direction.sqrMagnitude == 0f)
        {
            Debug.LogWarning("Target is exactly at the same position as the turret head.");
            return;
        }

        // Hedef yönüne bakacak þekilde rotasyonu hesapla
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Debug.Log("Current Target Direction: " + direction);


        // Yumuþak dönüþ için Lerp kullanarak rotasyonu hesapla
        TurretHead.transform.rotation = Quaternion.Lerp(TurretHead.transform.rotation, lookRotation, Time.deltaTime * 10f);

        Debug.Log("Current Target Location: " + currentTarget.transform.position);
        Debug.Log("Current Target Direction: " + direction);

    }

    void Fire()
    {
        Debug.Log("Firing... ");
        Instantiate(TurretBulletPrefab, FirePoint.transform.position, Quaternion.Euler(0, 0, 90));

        if(FirePoint2 != null)
        {
            Instantiate(TurretBulletPrefab, FirePoint2.transform.position, Quaternion.Euler(0, 0, 90));
            TurretBulletPrefab.GetComponent<Projectile>().SetDamage(bulletDamage);

        }
    }



    /*
    public override void AttackAnimationEvent()
    {
        StartCoroutine(WaitForAttack());
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        Instantiate(bulletPrefab, ShootPoint.position, Quaternion.LookRotation(direction));
    }


    private IEnumerator WaitForAttack()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
    }
    */
}
