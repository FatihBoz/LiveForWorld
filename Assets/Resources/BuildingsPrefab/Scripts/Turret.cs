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

    [Header("SOUND EFFECT")]
    private AudioSource audioSource;
    public AudioClip turretShootFx;

    public void Awake()
    {
        audioSource=GetComponent<AudioSource>();
    }
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

            // Ate� etme s�resi dolduysa, ate� et
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
        // Alg�lama alan�n� g�rselle�tirmek i�in bir �ember �izer
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
        print(currentTarget);
        // Hedefin y�n�n� hesapla
        Vector3 direction = currentTarget.transform.position - TurretHead.transform.position;
        // Yaln�zca Y ekseni �zerinde d�nd�rmek i�in Y d�zlemine projekte et

        if (direction.sqrMagnitude == 0f)
        {
            Debug.LogWarning("Target is exactly at the same position as the turret head.");
            return;
        }

        // Hedef y�n�ne bakacak �ekilde rotasyonu hesapla
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 lookRotationEu = lookRotation.eulerAngles;
        lookRotationEu.x = 270f;
        lookRotationEu.y += 180;
        lookRotation.eulerAngles=lookRotationEu;


        // Yumu�ak d�n�� i�in Lerp kullanarak rotasyonu hesapla
        TurretHead.transform.rotation = Quaternion.Lerp(TurretHead.transform.rotation, lookRotation, Time.deltaTime * 10f);

    }

    void Fire()
    {
        audioSource.PlayOneShot(turretShootFx);
        Vector3 lookRotationEu = TurretHead.transform.rotation.eulerAngles;
        lookRotationEu.x = 0;
        lookRotationEu.y += 180;
        GameObject bullet = Instantiate(TurretBulletPrefab, FirePoint.transform.position, Quaternion.Euler(lookRotationEu));


        if (FirePoint2 != null)
        {
            Instantiate(TurretBulletPrefab, FirePoint2.transform.position, Quaternion.Euler(lookRotationEu));
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
