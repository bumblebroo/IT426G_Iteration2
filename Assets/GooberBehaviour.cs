using UnityEngine;

public class GooberBehaviour : EnemyBehaviour {

    [SerializeField]
    private EnemyWanderState wanderState;

    [SerializeField]
    private EnemyMeleeAttackState meleeAttackState;

    [SerializeField]
    private EnemyChaseState chaseState;

    [SerializeField]
    private EnemyWaitState confusedState;

    protected override void Start() {
        wanderState.Initialize(this, Animator, Rb, Sr);
        meleeAttackState.Initialize(this, Animator, Rb, Sr);
        chaseState.Initialize(this, Animator, Rb, Sr);
        confusedState.Initialize(this, Animator, Rb, Sr);

        currentState = wanderState;
        base.Start();
    }

    protected override void HandleTransitions() {
        if(currentState == wanderState) {
            if (CanSeePlayer) {
                Transition(chaseState);
            }
            return;
        }

        if(currentState == chaseState) {
            if (CanAttack) {
                Transition(meleeAttackState);
            } else if (!CanFindPlayer) {
                Transition(confusedState);
            }
            return;
        }

        if(currentState == meleeAttackState) {
            if (!CanAttack) {
                Transition(chaseState);
            } else if (!CanFindPlayer) {
                Transition(confusedState);
            }
            return;
        }

        if(currentState == confusedState) {
            if (confusedState.DoTransition) {
                Transition(wanderState);
            } else if (CanSeePlayer) {
                Transition(chaseState);
            }
        }
    }
}
