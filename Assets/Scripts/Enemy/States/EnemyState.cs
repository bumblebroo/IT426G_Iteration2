using System;
using UnityEngine;

[Serializable]
public abstract class EnemyState
{
    private EnemyBehaviour enemyBehaviour;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("Animator")]
    [SerializeField]
    protected string animationTrigger;

    protected EnemyBehaviour EnemyBehaviour => enemyBehaviour;
    protected Animator Animator => animator;
    protected Rigidbody2D Rb => rb;
    protected SpriteRenderer Sr => sr;

    public virtual void Initialize(EnemyBehaviour enemyBehaviour, Animator animator, Rigidbody2D rb, SpriteRenderer sr) {
        this.enemyBehaviour = enemyBehaviour;
        this.animator = animator;
        this.rb = rb;
        this.sr = sr;
    }
    public virtual void EnterState() {
        animator.SetTrigger(animationTrigger);
    }
    public virtual void ExitState() {

    }
    public abstract void EnemyUpdate();

    protected virtual void Move(Vector2 desiredDirection, float speed, float acceleration) {
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desiredDirection * speed, acceleration * Time.deltaTime);
    }

    protected virtual void VelocityFlipSprite(Vector2 decisionBasis) {
        if (decisionBasis.x > 0) {
            sr.flipX = true;
        } else if (decisionBasis.x < 0) {
            sr.flipX = false;
        }
    }
}
