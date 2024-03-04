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

    [Header("Ground Check point")]
    [SerializeField] private Transform groundCheckPoint;

    private Rigidbody2D body;
    private Animator anim;
    private float horizontalInput;

    private bool onGround = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            if(body.velocity.magnitude <= 1f)
            {
                body.velocity = Vector2.zero;
            }
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
        Collider2D raycastHit = Physics2D.OverlapCircle(groundCheckPoint.position, 0.05f, groundLayer);
        return raycastHit != null;
    }
    
    public bool CanAttack()
    {
        return horizontalInput == 0 && IsOnGround();
    }
}