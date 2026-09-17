using System;
using UnityEngine;

[Serializable]
public abstract class PlayerMovementState : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    protected string animationTrigger;

    protected PlayerMovement playerMovement;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

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
