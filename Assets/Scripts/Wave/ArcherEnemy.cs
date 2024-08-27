using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform ShootPoint;

    protected override void Attack()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, ShootPoint.position, transform.rotation);
    }
}
