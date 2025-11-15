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

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;
    private float nextFire = 0f;

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
            transform.localScale = new Vector3(1, 1, 1);
        } else if (moveInput < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
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

        if (isShooting && Time.time > nextFire) {
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isJumping = false;
        bodyAnimator.SetBool("isJumping", false);
    }

    void Shoot() {
        float dir = transform.localScale.x > 0 ? 0f : 180f;

        Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.Euler(0, 0, dir)
        );
    }
}
