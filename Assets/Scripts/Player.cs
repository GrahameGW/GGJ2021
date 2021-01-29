using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jump = 5f;
    public int health = 3;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    bool isJumping = false;

    public UnityIntEvent OnHealthChange;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            rb.AddForce(Vector3.right * horizontal * speed, ForceMode2D.Impulse);
            sprite.flipX = horizontal < 0;
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

    public void Damage(int damage)
    {
        health -= damage;
        OnHealthChange.Invoke(health);
    }

    [ContextMenu("Damage")]
    public void OneDmgDebug()
    {
        Damage(1);
    }
}


