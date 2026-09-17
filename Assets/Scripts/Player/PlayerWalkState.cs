using System;
using UnityEngine;

[Serializable]
public class PlayerWalkState : PlayerMovementState
{
    [Header("Movement Values")]

    [SerializeField]
    [Min(0)]
    private float speed;

    [SerializeField]
    [Min(0)]
    [Range(0,1)]
    private float accelerationSpeed;

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
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, speed * desiredDirection, accelerationSpeed * Time.deltaTime);
    }
}
