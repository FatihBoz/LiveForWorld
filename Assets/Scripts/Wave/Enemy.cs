using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float timeBetweenPathUpdates = 0.5f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackDamage = 5f;


    protected Transform target;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected float lastPathUpdateTime;
    protected float attackTime = 0;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        animator = GetComponent<Animator>();
    }


    protected void FixedUpdate()
    {
        //Set enemies' destination in every certain amount of time instead of every frame.
        if (Time.time - lastPathUpdateTime >= timeBetweenPathUpdates)
        {
            DetectEnemies();

            lastPathUpdateTime = Time.time;
            if(target != null )
            {
                agent.SetDestination(target.position);
            }
        }

        if (target != null) 
        {

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange && Time.time >= attackTime + attackCooldown)
            {
                Attack();
                print("girdi1");
                attackTime = Time.time;
            }
        }
    }

    private void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Building") || (target == null && collider.CompareTag("Player")))
            {
                target = collider.transform;
                agent.SetDestination(target.position);
                print(agent.name);
            }
        }
    }


    protected virtual void Attack()
    {
        //animator.SetTrigger("EnemyAttack");
        print("attack");
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
