using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public float deathTime=1f;

    private float damage;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, deathTime);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
