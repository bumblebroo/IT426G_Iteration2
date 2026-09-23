using UnityEngine;

public class EnemyChaseState : EnemyState
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float acceleration; 

    public override void EnemyUpdate() {
        Vector2 playerDir = PlayerMovement.Instance.transform.position - EnemyBehaviour.transform.position;
        Move(playerDir, speed, acceleration);
    }
}
