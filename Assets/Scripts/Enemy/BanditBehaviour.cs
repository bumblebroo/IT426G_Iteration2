using UnityEngine;

public class BanditBehaviour : EnemyBehaviour
{
    [SerializeField]
    private EnemyWanderState wanderState;

    [SerializeField]
    private EnemyChaseState chaseState;

    [SerializeField]
    private EnemyRetreatState retreatState;

    [SerializeField]
    private EnemyWeapon weapon;

    protected override void Start() {
        wanderState.Initialize(this, Animator, Rb, Sr);
        chaseState.Initialize(this, Animator, Rb, Sr);
        retreatState.Initialize(this, Animator, Rb, Sr);

        currentState = wanderState;
        base.Start();
    }

    protected override void HandleTransitions() {
        if (currentState == wanderState) {
            weapon.NeutralAlign(Rb.linearVelocity.x);
            if (CanSeePlayer) {
                Transition(chaseState);
            }
            return;
        }

        weapon.ShootAtPlayer();

        if (currentState == chaseState) {
            if (CanAttack) {
                Transition(retreatState);
            } else if (!CanFindPlayer) {
                Transition(wanderState);
            }
            return;
        }

        if (currentState == retreatState) {
            if (!CanAttack) {
                Transition(chaseState);
            } else if (!CanFindPlayer) {
                Transition(wanderState);
            }
            return;
        }
    }
}
