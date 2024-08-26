using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{


    public float BulletSpeed = 5f;
    public GameObject BulletPrefab;
    public Transform ShootPoint;

    private Animator animator;

    private LayerMask layerMask;

    private int score=0;
    void Start()
    {
        layerMask= LayerMask.GetMask("Enemy");
        animator = GetComponent<Animator>();
        InputManager.Instance.input.Player.Shoot.performed += Shoot;
    }
      private void Shoot(InputAction.CallbackContext context)
    {
        animator.SetTrigger("Shoot");
        ScreenShake.Instance.TriggerShake();
        GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.rotation.x * BulletSpeed,0, bullet.transform.rotation.z * BulletSpeed);
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward,out hit,Mathf.Infinity,layerMask))
        {
            ScreenShake.Instance.TriggerShake();
            Destroy(hit.collider.gameObject);
            score+=10;
            Debug.Log(score);
        }


    }


}
