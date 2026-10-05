using System.Collections.Generic;
using UnityEngine;

public class EnemyRegistry : MonoBehaviour
{
    private static EnemyRegistry instance;

    [SerializeField]
    private Transform portalTransform;

    private HashSet<EnemyBehaviour> enemies = new HashSet<EnemyBehaviour>();

    public static EnemyRegistry Instance => instance;

    private void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public void RegisterEnemy(EnemyBehaviour enemy) {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyBehaviour enemy, bool checkIfLast = true) {
        enemies.Remove(enemy);
        if (!checkIfLast) {
            return;
        }

        if (!portalTransform) {
            return;
        }

        if(enemies.Count == 0) {
            portalTransform.position = enemy.transform.position;
            portalTransform.gameObject.SetActive(true);
        }
    }
}
