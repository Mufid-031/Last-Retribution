using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Animator bodyAnimator;
    private SpriteRenderer spriteBody;

    private float moveInput;
    private bool isShooting;
    private bool isJumping;
    private bool isCrounching;
    private bool isMelee;
    private bool isLookUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyAnimator = transform.Find("Body").GetComponent<Animator>();
        spriteBody = transform.Find("Body").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (!isCrounching) {
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
            bool isRunning = moveInput != 0;
            bodyAnimator.SetBool("isRunning", isRunning);
        }

        if (moveInput > 0) {
            spriteBody.flipX = false;
        } else if (moveInput < 0) { 
            spriteBody.flipX = true;
        }

        isShooting = Input.GetKey(KeyCode.J);
        bodyAnimator.SetBool("isShooting", isShooting);

        isCrounching = Input.GetKey(KeyCode.S);
        bodyAnimator.SetBool("isCrounching", isCrounching);

        isLookUp = Input.GetKey(KeyCode.W);
        bodyAnimator.SetBool("isLookUp", isLookUp);

        isMelee = Input.GetKey(KeyCode.K);
        bodyAnimator.SetBool("isMelee", isMelee);

        float vertical = Input.GetAxisRaw("Vertical");
        bodyAnimator.SetFloat("aimVertical", vertical);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) { 
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, 7f);
            isJumping = true;
            bodyAnimator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isJumping = false;
        bodyAnimator.SetBool("isJumping", false);
    }
}
