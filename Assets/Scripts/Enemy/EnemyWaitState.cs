using System;
using UnityEngine;

[Serializable]
public class EnemyWaitState : EnemyState
{
    [SerializeField]
    private float waitTime;

    private float startTimestamp;
    public bool DoTransition => Time.time - startTimestamp >= waitTime;
    public override void EnterState() {
        base.EnterState();
        startTimestamp = Time.time;
    }
    public override void EnemyUpdate() {
        // Do nothing
    }

}
