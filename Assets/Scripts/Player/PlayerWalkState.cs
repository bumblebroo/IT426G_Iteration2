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

    [SerializeField]
    [Min(0)]
    [Range(0, 1)]
    private float deAccelerationSpeed;

    [SerializeField]
    [Min(0)]
    private float animationThreshold;

    [SerializeField]
    private string walkMultiplierParameter;

    [SerializeField]
    private PlayerGunHandler playerGunHandler;

    [Header("Effects")]
    [SerializeField]
    private ParticleSystem walkParticles;

    public void OnValidate() {
        if (!walkParticles) {
            //Debug.LogError("Missing walkparticles");
        }
    }

    public override void Initialize(PlayerMovement movement, Animator animator, Rigidbody2D rb) {
        base.Initialize(movement, animator, rb);
    }
    public override void EnterState() {
        animator.SetTrigger(idleParameter);
    }
    public override void ExitState() {
        
    }
    public override void MovementUpdate(Vector2 desiredDirection) {
        bool walking = Mathf.Abs(rb.linearVelocity.magnitude) >= animationThreshold;
        animator.SetBool(animationTrigger, walking);

        /*
        if (walking && !walkParticles.isPlaying) {
            walkParticles.Play();
        } else {
            walkParticles.Stop();
        }
        */

        float diff = playerMovement.transform.position.x - playerGunHandler.MouseWorldPosition.x;
        if((rb.linearVelocity.x > 0) == (diff > 0)) {
            animator.SetFloat(walkMultiplierParameter, -1);
        } else {
            animator.SetFloat(walkMultiplierParameter, 1);
        }

        float lerpSpeed = accelerationSpeed;
        if(desiredDirection.magnitude == 0) {
            lerpSpeed = deAccelerationSpeed;
        }
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, speed * desiredDirection, lerpSpeed * Time.deltaTime);
    }
}
