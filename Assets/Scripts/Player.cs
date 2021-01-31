using UnityEngine;

public class Player : Creature {

    public ItemData HeldItem;

    public UnityStringEvent OnItemChanged;
    public UnityIntEvent OnHealthChangedUI;

    void Update() {
        PickAction();
        ApplyMovement(Input.GetAxis("Horizontal"));
    }

    public override void Damage(int damage)
    {
        health -= damage;
        OnHealthChangedUI.Invoke(health);
    }

    private void PickAction() {
        if (stateDelay <= 0) {
            switch (state) {
                case State.Normal: // can init action
                    if (Input.GetButtonDown("Jump")) {
                        if (onGround) {
                            ApplyJump();
                        } else {
                            ApplyGlide();
                        }
                    }
                    if (Input.GetButtonDown("Fire1")) {
                        ApplyAttack();
                    }

                    if (Input.GetButtonDown("Submit"))
                    {
                        HandleItem();
                    }
                    break;
                case State.Jumping:
                    EndJump();
                    if (Input.GetButtonDown("Jump"))
                    {
                        ApplyGlide();
                    }
                    break;
                case State.Gliding:
                    EndGlide();
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

    private void HandleItem()
    {
        if (HeldItem == null)
        {
            // pick up item if there
        }

        else
        {
            RoomManager.Instance.BuryItem(HeldItem);
            HeldItem = null;
        }

        string newItem = HeldItem?.name;
        OnItemChanged.Invoke(name);
    }
}

