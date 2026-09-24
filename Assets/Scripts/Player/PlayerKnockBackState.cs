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
            PlayerMovement.Transition(PlayerMovement.PlayerWalkState);
            return;
        }

        Rb.linearVelocity = currentForce;
        currentForce = Vector2.MoveTowards(currentForce, Vector2.zero, knockBackSlowdown * Time.deltaTime);
    }

    public void KnockBack(Vector2 force) {
        if(force.magnitude == 0) {
            return;
        }

        PlayerMovement.Transition(this);
        currentForce = force + (Rb.linearVelocity * Mathf.Clamp01(Vector2.Dot(Rb.linearVelocity, force)));
    }

    public void HitWall(Vector2 normal) {
        currentForce += normal.normalized * currentForce.magnitude;
    }
}
