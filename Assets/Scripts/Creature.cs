using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpHeight = 5f;
    public int health = 3;

    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    protected bool onGround;



    protected enum State
    {
        Normal,
        Jumping,
        Gliding,
        Attacking
    }
    protected State state;

    public int jumpDelay = 3;
    public int AttackDelay = 5;
    protected int stateDelay = 0;

    public UnityIntEvent OnHealthChange;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    protected void ApplyMovement(float horizontal)
    {
        if (horizontal != 0)
        {
            transform.Translate(Vector3.right * horizontal * speed);
            sprite.flipX = horizontal < 0;
        }
    }


    protected void ApplyJump()
    {
        state = State.Jumping;
        stateDelay = jumpDelay;
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    protected void ApplyGlide()
    {
        state = State.Gliding;
        //TODO
    }

    protected void ApplyAttack()
    {
        state = State.Attacking;
        stateDelay = jumpDelay;
        //TODO
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


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 11)
        { // ground
            onGround = true;
            //animator.SetBool("Fall", false);
            //audioSource.PlayOneShot(landingClip);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        { // ground
            onGround = false;
            //animator.SetBool("Fall", true);
            //audioSource.Pause(); // no running noises on the ground
        }
    }

}

[CreateAssetMenu(menuName = "Level Map")]
public class LevelMap : ScriptableObject
{
    public Room[,] rooms;
}
