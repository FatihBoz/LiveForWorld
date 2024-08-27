using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public float AttackSpeed = 4f; 
    public float BulletDamage;
    public float BulletSpeed = 5f;
    public GameObject BulletPrefab;
    public Transform ShootPoint;
    public AudioClip ShootSound;

    private Animator animator;

    private SoundEffect sf;
    private bool isShooting;

    void Start()
    {
        animator = GetComponent<Animator>();
        sf = GetComponent<SoundEffect>();

        // Input Manager'dan tetikleme
        InputManager.Instance.input.Player.Shoot.performed += StartShooting;
        InputManager.Instance.input.Player.Shoot.canceled += EndShooting;
    }

    private void EndShooting(InputAction.CallbackContext context)
    {
        isShooting = false;
        print("end");
    }

    private void StartShooting(InputAction.CallbackContext context)
    {
        isShooting = true;
        StartCoroutine(StartShootingRoutine());
    }



    private IEnumerator StartShootingRoutine()
    {
        while (isShooting)
        {
            animator.SetTrigger("Shoot");

            sf.PlaySoundEffect(ShootSound);

            ScreenShake.Instance.TriggerShake();

            GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position, transform.rotation);
            bullet.GetComponent<Projectile>().SetDamage(BulletDamage);

            yield return new WaitForSeconds(1/AttackSpeed);
        }
    }
}
    



