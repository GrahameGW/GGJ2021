using UnityEngine;

public class Player : Creature {

    void Update() {
        PickAction();
        ApplyMovement(Input.GetAxis("Horizontal"));
    }


    private void PickAction() {
        if (stateDelay == 0) {
            switch (state) {
                case State.Normal: // can init action
                    if (Input.GetButtonDown("Jump")) {
                        if (onGround) {
                            ApplyJump();
                        } else {
                            ApplyGlide();
                        }
                    }
                    if (Input.GetButtonDown("Attack")) {
                        ApplyAttack();
                    }
                    break;
                case State.Jumping:
                    EndJump();
                    break;
                case State.Gliding:
                    if (!Input.GetButtonDown("Jump") || onGround) {
                        EndGlide();
                    }
                    break;
                case State.Attacking:
                    EndAttack();
                    break;
                default:
                    Debug.LogError("Player in unimplemented state.");
                    break;
            }
        } else if (state == State.Normal) { // shouldn't get here but for safety
            stateDelay = 0;
        }
        stateDelay -= 1; // decrement the state delay counter;
    }
}

