using System;
using UnityEngine;

[Serializable]
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
        dir = new Vector2(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f).normalized;
    }

    public override void EnemyUpdate() {
        if(Time.time - newDirTimestamp > newDirCD) {
            dir = new Vector2(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f).normalized;
            newDirTimestamp = Time.time;
        }

        Move(dir, speed, acceleration);
    }
}
