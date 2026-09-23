using UnityEngine;

public class EnemyWanderState : EnemyState
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float newDirCD;

    private Vector2 dir;
    private float newDirTimestamp;

    public override void EnterState() {
        base.EnterState();
        newDirTimestamp = 0;
    }

    public override void EnemyUpdate() {
        if(newDirTimestamp - Time.time > newDirCD) {
            dir = new Vector2(Random.value / 2, Random.value / 2).normalized;
        }

        Move(dir, speed, acceleration);
    }
}
