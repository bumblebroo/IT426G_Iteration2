using System;
using UnityEngine;

[Serializable]
public class PlayerWalkState : PlayerMovementState
{
    [SerializeField]
    private string idleParameter;

    [Header("Movement Values")]

    [SerializeField]
    [Min(0)]
    private float speed;

    [SerializeField]
    [Min(0)]
    [Range(0,1)]
    private float accelerationSpeed;

    public override void Initialize(PlayerMovement movement, Animator animator, Rigidbody2D rb) {
        base.Initialize(movement, animator, rb);
    }
    public override void EnterState() {
        animator.SetTrigger(idleParameter);
    }
    public override void ExitState() {
        
    }
    public override void MovementUpdate(Vector2 desiredDirection) {
        animator.SetFloat(animationTrigger, Mathf.Abs(rb.linearVelocity.magnitude));

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, speed * desiredDirection, accelerationSpeed * Time.deltaTime);
    }
}
