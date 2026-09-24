using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyMeleeAttackState : EnemyState
{
    [SerializeField]
    private float attackCD;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float deacceleration;

    [SerializeField]
    private Collider2D attackCollider;
    private float attackTimeStamp;

    public override void EnterState() {
        // Do nothing
        attackTimeStamp = Time.time - attackCD - 1;
    }

    public override void EnemyUpdate() {
        Move(Vector2.zero, 0, deacceleration);

        if(Time.time - attackTimeStamp < attackCD) {
            return;
        }

        base.EnterState();

        List<Collider2D> colliders = new List<Collider2D>();
        attackCollider.Overlap(colliders);

        for (int i = 0; i < colliders.Count; i++) {
            HealthBase health;
            if (!colliders[i].gameObject.TryGetComponent<HealthBase>(out health)) {
                continue;
            }

            health.TakeDamage(damage);
        }

        attackTimeStamp = Time.time;
    }
}
