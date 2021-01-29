using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jump = 5f;

    private Rigidbody2D rb;
    bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            transform.Translate(Vector3.right * horizontal * speed);
        }

        if (Input.GetAxis("Jump") > 0 && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
}
