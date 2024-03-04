using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private Projectile projectilePrefab;
    private ObjectPool<Projectile> projectilePool;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        projectilePool = new ObjectPool<Projectile>(projectilePrefab, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack()
            && Time.timeScale > 0)
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.Instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        Projectile projectile = projectilePool.GetObjectFromPool();
        projectile.transform.position = firePoint.position;
        projectile.SetProjectile(Mathf.Sign(transform.localScale.x), projectilePool);
    }
}