using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public Transform attackPoint;
    public float attackRangeForSphere;
  public override void AttackAnimationEvent()
    {
        StartCoroutine(WaitForAttack());
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRangeForSphere,focusLayerMask);
         foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
               PlayerProperties.Instance.DecreasePlayerHealth(attackDamage);
            }
            else
            {
                collider.GetComponent<Building>().ChangeHealth((int)attackDamage);
            }
        }
    }
    private IEnumerator WaitForAttack()
    {
            astarAI.SetRunning(false);

     //   agent.isStopped = true;
        yield return new WaitForSeconds(1);
      //  agent.isStopped = false;
            astarAI.SetRunning(true);

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(attackPoint.position,attackRangeForSphere);
    }
}
