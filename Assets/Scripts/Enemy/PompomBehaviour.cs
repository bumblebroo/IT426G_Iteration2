using UnityEngine;

public class PompomBehaviour : EnemyBehaviour
{
    [SerializeField]
    private EnemyWanderState wanderState;

    [SerializeField]
    private EnemyChaseState chaseState;

    [SerializeField]
    private EnemySelfDestructState selfDestructState;

    protected override void Start() {
        wanderState.Initialize(this, Animator, Rb, Sr);
        chaseState.Initialize(this, Animator, Rb, Sr);
        selfDestructState.Initialize(this, Animator, Rb, Sr);

        currentState = wanderState;
        base.Start();
    }

    protected override void HandleTransitions() {
        if (currentState == wanderState) {
            if (CanSeePlayer) {
                Transition(chaseState);
            }
            return;
        }

        if (currentState == chaseState) {
            if (CanAttack) {
                Transition(selfDestructState);
            } else if (!CanFindPlayer) {
                Transition(wanderState);
            }
            return;
        }
    }
}
