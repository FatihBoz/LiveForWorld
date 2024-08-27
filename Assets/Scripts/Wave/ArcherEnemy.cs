using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform ShootPoint;

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
}
