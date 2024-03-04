using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;
    private ObjectPool<EnemyProjectile> projectilePool;
    private Vector3 direction;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void SetProjectile(Vector3 direction, ObjectPool<EnemyProjectile> projectilePool)
    {
        this.direction = direction;
        hit = false;
        lifetime = 0;
        coll.enabled = true;
        this.projectilePool = projectilePool;
    }

    private void Update()
    {
        if (hit) return;
        transform.Translate(direction * speed* Time.deltaTime);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
        coll.enabled = false;

        if (anim != null)
        {
            anim.SetTrigger("explode");
        }
        else
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        projectilePool.ReturnObjectToPool(this);
    }
}