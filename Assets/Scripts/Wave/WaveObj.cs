using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveObj : MonoBehaviour
{
    protected Transform target;
    protected NavMeshAgent agent;
    public void Awake()
    {
        agent=GetComponent<NavMeshAgent>();
    }
    public virtual void FixedUpdate()
    {
        agent.SetDestination(target.position);
    }
    public void SetTarget(Transform target)
    {
        this.target=target;
    }
}
