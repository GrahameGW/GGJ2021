using UnityEngine;

public class Player : Creature
{

    void Update()
    {
        PickAction();
        ApplyMovement(Input.GetAxis("Horizontal"));
    }


    private void PickAction()
    {
        switch (state)
        {
            case State.Gliding:
                if (!Input.GetButtonDown("Jump") || onGround)
                {
                    state = State.Normal;
                }
                break;
            case State.Normal:
                if (Input.GetButtonDown("Jump"))
                {
                    if (onGround)
                    {
                        ApplyJump();
                    }
                    else
                    {
                        ApplyGlide();
                    }
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    ApplyAttack();
                }
                break;
            default:
                if (stateDelay == 0)
                {
                    state = State.Normal;
                }
                break;
        }
    }
}

