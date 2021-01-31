using UnityEngine;

public class PatrolEnemy : Creature
{

    [SerializeField] Transform groundDetect;
    [SerializeField] float groundDetectRange = 2f;
    [SerializeField] float playerDetectRange = 5f;
    protected bool faceRight = false;

    // Update is called once per frame
    void Update()
    {
        switch (state){
            case State.Normal:
                if (!DetectPlayer()) {
                    Move();
                }
                break;
            case State.Attacking:
                if (stateDelay <= 0) {
                    EndAttack();
                } else {
                    stateDelay -= 1;
                }
                break;
            default:
                Debug.LogError("Unimplemented state");
                break;
        }
        

    }

    bool DetectPlayer() {
        if (state == State.Normal) {
            RaycastHit2D playerCheck = Physics2D.Raycast(this.gameObject.transform.position, Vector2.left, playerDetectRange, LayerMask.GetMask("Default"));
            if (playerCheck.rigidbody != null) {
                Debug.Log("there was a left hit");
            }
            if (playerCheck.rigidbody != null && playerCheck.transform.gameObject.CompareTag("Player")) {
                Debug.Log("player detected to left");
                ApplyMovement(-1f);
                ApplyAttack();
                return true;
            }
            playerCheck = Physics2D.Raycast(this.gameObject.transform.position, Vector2.right, playerDetectRange, LayerMask.GetMask("Default"));
            if (playerCheck.transform != null) {
                Debug.Log("there was a right hit");
            }
            if (playerCheck.transform != null && playerCheck.transform.gameObject.CompareTag("Player")) {
                Debug.Log("player detected to right");
                ApplyMovement(1f);
                ApplyAttack();
                return true;
            }
        }
        return false;
        
    }


    protected void Move() {
        //RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, groundDetectRange);
        ////RaycastHit2D wallCheck = Physics2D.Raycast(groundDetect.position, faceRight ? Vector2.right : Vector2.left, 0.05f);
        ////if (!groundCheck.collider || (wallCheck.collider && (wallCheck.collider.gameObject.layer == 11 || wallCheck.collider.gameObject.layer == 8))) {
        ////    faceRight = !faceRight;
        ////    groundDetect.localPosition = Vector2.Reflect(groundDetect.localPosition, Vector2.right);
        ////}
        //if (!groundCheck.collider)
        //{
        //    faceRight = !faceRight;
        //  groundDetect.localPosition = Vector2.Reflect(groundDetect.localPosition, Vector2.right);
        //}
        if (faceRight) {
            ApplyMovement(3f);
            sprite.flipX = true;
        } else {
            ApplyMovement(-3f);
            sprite.flipX = false;
        }
        
    }
}
