using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    private Rigidbody2D rb;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
