using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveObj : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public void Start()
    {
        agent=GetComponent<NavMeshAgent>();
    }
    public void FixedUpdate()
    {
        agent.SetDestination(target.position);
    }
}
