using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : Building
{
    public GameObject TurretBulletPrefab;

    public Transform FirePoint;

    public Transform FirePoint2;

    public float detectionRadius = 10f;

    GameObject currentTarget;

    public GameObject TurretHead;

    public Animator TurretHeadAnimator;

    private float fireCountdown = 0f; 
    public float fireRate = 1f;
    public float bulletDamage = 10;

    [Header("SOUND EFFECT")]
    private AudioSource audioSource;
    public AudioClip turretShootFx;

    private PlayerController player;

    public void Awake()
    {
        player=FindObjectOfType<PlayerController>();
        audioSource=GetComponent<AudioSource>();
    }
    public override void UpgradeBuilding()
    {
        if(PlayerProperties.Instance.getOre() < cost)
        {
            Debug.Log("Not Enough Ore");
            return;
        }

        if (level < 3)
        {
            PlayerProperties.Instance.ChangeOreAmount(-cost);

            level++;

            health += 50;

            maxHealth += 50;

            bulletDamage += 50;

            Debug.Log("Maden seviyesi y�kseltildi! Yeni seviye: " + level);

            GameObject temp = Instantiate(BuildingsPrefab.Instance.turretLevels[level-1], this.transform.position, this.transform.rotation);
            temp.GetComponent<Building>().level = level;


            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }

    void Update()
    {
        if(currentTarget == null)
        {
            FindNearestEnemy();
            TurretHeadAnimator.SetBool("FoundEnemy", false); 
        }
        else
        {
            Vector3 distance = currentTarget.transform.position - transform.position;
            distance.y=0;
            if (distance.magnitude>=detectionRadius)
            {
                currentTarget=null;
            }

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


        if (Input.GetKeyDown(KeyCode.Q) && isPressed)
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
        print("direction : " + direction);
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
        //TurretHead.transform.rotation = Quaternion.Lerp(TurretHead.transform.rotation, lookRotation, Time.deltaTime * 10f);
        TurretHead.transform.rotation = lookRotation;

    }

    void Fire()
    {
        audioSource.PlayOneShot(turretShootFx);
        Vector3 lookRotationEu = TurretHead.transform.rotation.eulerAngles;
        lookRotationEu.x = 0;
        lookRotationEu.y += 180;
        Vector3 direction = currentTarget.transform.position-FirePoint.transform.position;
        GameObject bullet = Instantiate(TurretBulletPrefab, FirePoint.transform.position, Quaternion.LookRotation(direction));
        bullet.GetComponent<Projectile>().SetDamage(bulletDamage);

        if (FirePoint2 != null)
        {
        direction = currentTarget.transform.position-FirePoint2.transform.position;

            GameObject bullet2=Instantiate(TurretBulletPrefab, FirePoint2.transform.position, Quaternion.LookRotation(direction));
            bullet2.GetComponent<Projectile>().SetDamage(bulletDamage);
        }
    }


}
