using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherWaveObj : WaveObj
{
    public float distanceBetweenTarget;

    public GameObject bulletPrefab;

    public float attackCooldown=5f;
    private float attackTime=0;
    public void Start()
    {
        agent.SetDestination(target.position);
    }
    public override void FixedUpdate()
    {
        Vector3 distance = target.position-transform.position;
        distance.y=0;
        if (distance.magnitude<distanceBetweenTarget)
        {
            agent.isStopped=true;
            AttackToTarget();
        }
        else
        {
            agent.isStopped=false;
            agent.SetDestination(target.position);
        }
    }
    public void AttackToTarget()
    {
        if (Time.time>=attackTime+attackCooldown)
        {
            attackTime=Time.time;
            Vector3 direction=target.position-transform.position;
            direction.y=0;
            direction.Normalize();
            GameObject bullet = Instantiate(bulletPrefab, transform.position+2*direction, transform.rotation);
            bullet.transform.rotation=Quaternion.LookRotation(direction,Vector3.up);
            bullet.GetComponent<Rigidbody>().AddForce(direction*Time.deltaTime);
        }
    }
}
