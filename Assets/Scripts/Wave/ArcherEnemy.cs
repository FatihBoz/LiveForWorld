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
        direction.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, ShootPoint.position, Quaternion.LookRotation(direction));
        bullet.GetComponent<EnemyProjectile>().SetDamage(attackDamage);
    }
    private IEnumerator WaitForAttack()
    {
            astarAI.SetRunning(false);

     //   agent.isStopped = true;
        yield return new WaitForSeconds(1);
      //  agent.isStopped = false;
            astarAI.SetRunning(true);

    }
}
