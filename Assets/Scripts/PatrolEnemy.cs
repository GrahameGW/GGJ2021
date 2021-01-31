using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Creature
{

    [SerializeField] Transform groundDetect;
    [SerializeField] float groundDetectRange = 2f;
    [SerializeField] float playerDetectRange = 5f;
    protected bool faceRight = true;

    // Update is called once per frame
    void Update()
    {
        switch (state){
            case State.Normal:
                if (!detectPlayer()) {
                    move();
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

    bool detectPlayer() {
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


    protected void move() {
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, groundDetectRange);
        if (!groundCheck.collider) {
            faceRight = !faceRight;
            groundDetect.localPosition = Vector2.Reflect(groundDetect.localPosition, Vector2.right);
        }
        if (faceRight) {
            ApplyMovement(1f);
        } else {
            ApplyMovement(-1f);
        }
        
    }
}
