using System;
using UnityEngine;

[Serializable]
public class PlayerDashState : PlayerMovementState {
    [Header("Dash Values")]

    [SerializeField]
    private float dashForce;

    [SerializeField]
    private float dashDuration;

    private float time = 0;
    private Vector2? targetDir;

    public override void OnValidate(GameObject source) {

    }
    public override void Initialize(PlayerData data, PlayerMovement movement) {
        base.Initialize(data, movement);
    }
    public override void EnterState(Animator animator) {
        base.EnterState(animator);
        time = 0;
    }
    public override void ExitState() {
        targetDir = null;
    }
    public override void MovementUpdate(Rigidbody2D rb, Vector2 desiredDirection) {
        if(targetDir == null) {
            targetDir = desiredDirection;
        }

        if(time >= dashDuration) {
            playerMovement.Transition(playerMovement.PlayerWalkState);
        }
    }
}
