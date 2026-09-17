using System;
using UnityEngine;

[Serializable]
public class PlayerKnockBackState : PlayerMovementState {

    [SerializeField]
    private float knockBackSlowdown;

    [SerializeField]
    [Min(0)]
    private float knockBackEndThreshold;

    private Vector2 currentForce;

    public override void Initialize(PlayerMovement movement, Animator animator, Rigidbody2D rb) {
        base.Initialize(movement, animator, rb);
    }
    public override void EnterState() {
        base.EnterState();
    }
    public override void ExitState() {

    }
    public override void MovementUpdate(Vector2 desiredDirection) {
        if(currentForce.magnitude <= knockBackEndThreshold) {
            playerMovement.Transition(playerMovement.PlayerWalkState);
            return;
        }

        rb.linearVelocity = currentForce;
        currentForce = Vector2.MoveTowards(currentForce, Vector2.zero, knockBackSlowdown * Time.deltaTime);
    }

    public void KnockBack(Vector2 force) {
        playerMovement.Transition(this);
        currentForce = force;
    }
}
