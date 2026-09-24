using System;
using UnityEngine;

[Serializable]
public class EnemyChaseState : EnemyState
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float acceleration;

    public override void EnterState() {
        base.EnterState();
    }

    public override void EnemyUpdate() {
        Vector2 playerDir = EnemyBehaviour.PlayerPosKnowledge - (Vector2)EnemyBehaviour.transform.position;
        Move(playerDir, speed, acceleration);
    }
}
