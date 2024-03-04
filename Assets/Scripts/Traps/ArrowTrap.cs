using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Vector3 direction;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    [SerializeField] private EnemyProjectile projectilePrefab;
    private ObjectPool<EnemyProjectile> projectilePool;

    private void Awake()
    {
        projectilePool = new ObjectPool<EnemyProjectile>(projectilePrefab, 10);
    }

    private void Attack()
    {
        cooldownTimer = 0;

        SoundManager.Instance.PlaySound(arrowSound);

        EnemyProjectile projectile = projectilePool.GetObjectFromPool();
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        projectile.SetProjectile(direction, projectilePool);
    }
   
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}