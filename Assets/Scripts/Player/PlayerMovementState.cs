using System;
using UnityEngine;

[Serializable]
public abstract class PlayerMovementState
{
    [Header("Animator")]
    [SerializeField]
    protected string animationTrigger;

    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    protected PlayerMovement PlayerMovement => playerMovement;
    protected Animator Animator => animator;
    protected Rigidbody2D Rb => rb;
    protected SpriteRenderer Sr => sr;

    public virtual void Initialize(PlayerMovement movement, Animator animator, Rigidbody2D rb) {
        playerMovement = movement;
        this.animator = animator;
        this.rb = rb;
    }
    public virtual void EnterState() {
        animator.SetTrigger(animationTrigger);
    }
    public abstract void ExitState();
    public abstract void MovementUpdate(Vector2 desiredDirection);
}
