using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour {
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField]
    private float glideSpeed = 0f; // how fast you fall when gliding


    [SerializeField] GameObject attackFab;
    [SerializeField] Transform attackOffest; //Where to spawn attack in relation to creature
    [SerializeField] int maxHealth = 3;
    public int health = 3; // current health


    // delays = how long after init. action can you preform next one
    // basically length of animation + any freeze time
    public int jumpDelay = 3;
    public int attackDelay = 5;
    //public int glideDelay = 0;



    protected enum State {
        Normal,
        Jumping,
        Gliding,
        Attacking
    }

    protected State state;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    protected bool onGround;

    protected GameObject attackObj;

    protected int stateDelay = 0; // counter for current state

    public UnityEvent OnHealthChange;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (OnHealthChange is null) {
            OnHealthChange = new UnityEvent();
            OnHealthChange.AddListener(UpdateHealth);
        }
        
    }


    protected void ApplyMovement(float horizontal) {
        if (horizontal != 0) {
            sprite.flipX = horizontal < 0;
        }

        if (state == State.Gliding && rb.velocity.y < 0f && Mathf.Abs(rb.velocity.y) > glideSpeed) {
            rb.velocity = new Vector2(horizontal, Mathf.Sign(rb.velocity.y) * glideSpeed);
        } else {
            transform.Translate((horizontal * Time.deltaTime * speed), 0f, 0f);
        }
    }


    protected void ApplyJump() {
        state = State.Jumping;
        stateDelay = jumpDelay;
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    protected void EndJump() {
        state = State.Normal;
    }

    protected void ApplyGlide() {
        state = State.Gliding;
        //TODO
    }

    protected void EndGlide() {
        state = State.Normal;
    }

    protected void ApplyAttack() {
        state = State.Attacking;
        stateDelay = attackDelay;
        attackObj = Instantiate(attackFab, attackOffest);
        AttackData data = attackObj.GetComponent<AttackData>();
        data.owner = this.GetInstanceID();
    }

    protected void EndAttack() {
        Destroy(attackObj);
        state = State.Normal;
    }


    public void Damage(int damage) {
        health -= damage;
        OnHealthChange.Invoke();
    }

    [ContextMenu("Kill")]
    public void Kill() {
        maxHealth = -1;
        OnHealthChange.Invoke();
    }

    [ContextMenu("Damage")]
    public void OneDmgDebug() {
        Damage(1);
    }


    void UpdateHealth() {
        Debug.Log("Health update triggered");
        health = health > maxHealth ? maxHealth : health;
        if (health <= 0) {
            Die();
        }
    }


    protected void Die() {
        Debug.Log($"{this.GetType()} ({this.GetInstanceID()}) has died.");
        //TODO: on death stuff
        Destroy(this.transform.parent);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.layer) {
            case 11: // ground             
                onGround = true;        
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        switch (collision.gameObject.layer) {
            case 11: // ground   
                onGround = false;
                break;
        }
    }

}
