using System;
using UnityEngine;

[Serializable]
public class EnemySelfDestructState : EnemyState
{
    [SerializeField]
    private float explosionDelay;

    [SerializeField]
    private GameObject explosionPrefab; 

    private float startTimeStamp = 0;
    public override void EnterState() {
        base.EnterState();
        startTimeStamp = Time.time;
    }
    public override void EnemyUpdate() {
        if(Time.time - startTimeStamp < explosionDelay) {
            return;
        }

        GameObject.Instantiate(explosionPrefab, EnemyBehaviour.transform.position, Quaternion.identity);
        GameObject.Destroy(EnemyBehaviour.gameObject);
    }
}
