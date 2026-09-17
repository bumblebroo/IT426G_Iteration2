using System;
using UnityEngine;

[Serializable]
public class PlayerKnockBackState : PlayerMovementState {
    public override void OnValidate(GameObject source) {

    }
    public override void Initialize(PlayerData data, PlayerMovement movement) {
        base.Initialize(data, movement);
    }
    public override void EnterState(Animator animator) {
        base.EnterState(animator);
    }
    public override void ExitState() {

    }
    public override void MovementUpdate(Rigidbody2D rb, Vector2 desiredDirection) {

    }
}
