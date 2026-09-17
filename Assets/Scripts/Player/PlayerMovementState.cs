using System;
using UnityEngine;

[Serializable]
public abstract class PlayerMovementState
{
    [Header("Animator")]
    [SerializeField]
    private string animationTrigger;

    protected PlayerData playerData;
    protected PlayerMovement playerMovement;

    public abstract void OnValidate(GameObject source);
    public virtual void Initialize(PlayerData data, PlayerMovement movement) {
        playerData = data;
        playerMovement = movement;
    }
    public virtual void EnterState(Animator animator) {
        animator.SetTrigger(animationTrigger);
    }
    public abstract void ExitState();
    public abstract void MovementUpdate(Rigidbody2D rb, Vector2 desiredDirection);
}
