using UnityEngine;

public class LootChest : MonoBehaviour
{
    [SerializeField]
    private LootTable lootTable;

    [SerializeField]
    private int minLootAmount, maxLootAmount;

    private bool opened = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (opened) {
            return;
        }
        
        if (!collision.gameObject.GetComponent<PlayerMovement>()) {
            return;
        }

        int amount = Random.Range(minLootAmount, maxLootAmount);

        for (int i = 0; i < amount; i++) {
            SpawnLoot();
        }

        opened = true;
    }

    private void SpawnLoot() {
        Instantiate(lootTable.GetPrefab(), transform.position, Quaternion.identity);
    }
}
