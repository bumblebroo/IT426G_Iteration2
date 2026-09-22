using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerDashState : PlayerMovementState {
    [Header("Dash Values")]

    [SerializeField]
    [Min(0)]
    private float dashForce;

    [SerializeField]
    [Min(0)]
    private float dashDuration;

    [SerializeField]
    [Min(0)]
    private float dashCooldown;

    [SerializeField]
    private AnimationCurve speedFalloff;

    private float time = 0;
    private float lastDash = 0;
    private Vector2 targetDir;

    public bool CanDash => Time.time - lastDash >= dashCooldown;

    public override void Initialize(PlayerMovement movement, Animator animator, Rigidbody2D rb) {
        base.Initialize(movement, animator, rb);
    }

    public override void EnterState() {
        if (!CanDash) {
            playerMovement.Transition(playerMovement.PlayerWalkState);
        }

        base.EnterState();
        time = 0;
    }
    public override void ExitState() {
        targetDir = Vector2.zero;
    }
    public override void MovementUpdate(Vector2 desiredDirection) {
        if(targetDir == Vector2.zero) {
            if(desiredDirection == Vector2.zero) {
                playerMovement.Transition(playerMovement.PlayerWalkState);
                return;
            }
            targetDir = desiredDirection;
        }

        if(time >= dashDuration) {
            playerMovement.Transition(playerMovement.PlayerWalkState);
            lastDash = Time.time;
            return;
        }

        rb.linearVelocity = targetDir * speedFalloff.Evaluate(time / dashDuration) * dashForce;

        time += Time.deltaTime;
    }
}
