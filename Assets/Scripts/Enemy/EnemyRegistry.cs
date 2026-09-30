using System.Collections.Generic;
using UnityEngine;

public class EnemyRegistry : MonoBehaviour
{
    private static EnemyRegistry instance;

    [SerializeField]
    private GameObject portalPrefab;

    [SerializeField]
    private string nextSceneName;

    private List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();

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

        if(enemies.Count == 0) {
            GameObject portal = Instantiate(portalPrefab, enemy.transform.position, Quaternion.identity);
            portal.GetComponent<LoadLevelOnTrigger>().SetNextSceneName(nextSceneName);
        }
    }
}
