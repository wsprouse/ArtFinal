using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Animation Variables
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    //jumping variables
    public Rigidbody2D rb;
    public float jumpForce = 3f;
    private bool isOnGround = true;

    [SerializeField] private float speed;
    private Rigidbody2D body;

    private float horizontalInput;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput>0)
        {
            spriteRenderer.flipX = false;
        }else if(horizontalInput<0)
        {
            spriteRenderer.flipX = true;
        }


        body.velocity = new Vector2(horizontalInput* speed, body.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));



        if (Input.GetKeyDown(KeyCode.Space)  && isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
            animator.SetBool("isOnGround", false);
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isOnGround", true);
        }
    }
}