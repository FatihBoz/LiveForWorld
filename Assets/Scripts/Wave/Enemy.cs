using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float timeBetweenPathUpdates = 0.5f;
    [SerializeField] protected float attackRange = 5f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private GameObject bloodEffect;
    public AudioClip getHitSoundFx;

    protected Transform target;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected AudioSource audioSource;
    
    protected float lastPathUpdateTime;
    protected float attackTime = 0;
    private float currentHp;

    private bool isAlive;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        currentHp = maxHp;
        isAlive=true;
    }


    protected void FixedUpdate()
    {
        if (!isAlive)
        {
            agent.isStopped=true;
            return;
        }

        Animate();

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
            RotateTowardsPlayer(target.transform);
            CalculateDistanceAndAttack();
        }


    }

    private void RotateTowardsPlayer(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
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
            }
        }
    }

    private void CalculateDistanceAndAttack()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }


        if (distanceToTarget <= attackRange && Time.time >= attackTime + attackCooldown)
        {
            Attack();
        }
    }


    public virtual void AttackAnimationEvent()
    {
        print("atak");
    }

    private void Attack()
    {
        animator.SetTrigger("EnemyAttack");
        attackTime = Time.time;
    }

    private void Animate()
    {
        animator.SetFloat("Speed",agent.speed);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHp -= damageAmount;
        audioSource.PlayOneShot(getHitSoundFx);
        if(currentHp <= 0)
        {

            isAlive=false;
            WaveSpawn.Instance.DecreaseWaveObjCount();
            //Die Animation
            agent.isStopped=true;
            Destroy(GetComponent<Collider>());
            animator.SetTrigger("Die");
        }
    }

    public void DestroyCh()
    {
        Destroy(gameObject,0.3f);
    }


    public GameObject GetBloodEffect()
    {
        return bloodEffect;
    }
}
