using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public float deathTime=1f;

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
}
