using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{

    public float BulletDamage;
    public float BulletSpeed = 5f;
    public GameObject BulletPrefab;
    public Transform ShootPoint;
    public AudioClip ShootSound;

    private Animator animator;

    private LayerMask layerMask;
    private SoundEffect sf;
    private int score=0;
    void Start()
    {
        layerMask= LayerMask.GetMask("Enemy");
        animator = GetComponent<Animator>();
        sf=GetComponent<SoundEffect>();
        InputManager.Instance.input.Player.Shoot.performed += Shoot;
    }
      private void Shoot(InputAction.CallbackContext context)
    {
        animator.SetTrigger("Shoot");
        sf.PlaySoundEffect(ShootSound);
        ScreenShake.Instance.TriggerShake();
        GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position, transform.rotation);
        bullet.GetComponent<Projectile>().SetDamage(BulletDamage);
    }


}
