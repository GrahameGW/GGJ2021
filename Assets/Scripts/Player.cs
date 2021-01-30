using UnityEngine;

public class Player : Creature
{

    void Update()
    {
        pickAction();
        ApplyMovement(Input.GetAxis("Horizontal"));
    }


    private void pickAction()
    {
        switch (state)
        {
            case State.Gliding:
                if (!Input.GetKey("Jump") || onGround)
                {
                    state = State.Normal;
                }
                break;
            case State.Normal:
                if (Input.GetKeyDown("Jump"))
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
                if (Input.GetKeyDown("Fire1"))
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
