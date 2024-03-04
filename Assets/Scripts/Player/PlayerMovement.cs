using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private bool onGround;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        onGround = IsOnGround();
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput != 0)
        {
            Flip();
            Move();
        }
        else
        {
            body.velocity = Vector2.zero;
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", onGround);
    }

    private void Move()
    {
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    private void Flip()
    {
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Jump()
    {
        SoundManager.Instance.PlaySound(jumpSound);
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    
    public bool CanAttack()
    {
        return horizontalInput == 0 && IsOnGround();
    }
}