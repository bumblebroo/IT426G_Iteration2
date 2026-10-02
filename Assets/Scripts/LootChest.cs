using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LootChest : MonoBehaviour
{
    [SerializeField]
    private LootTable lootTable;

    [SerializeField]
    private int minLootAmount, maxLootAmount;

    [Space]

    [SerializeField]
    private float awayPushSpeed;

    [SerializeField]
    private Sprite openSprite;

    public UnityEvent ChestOpened;

    private bool opened = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (opened) {
            return;
        }
        
        if (!collision.gameObject.GetComponent<PlayerMovement>()) {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = openSprite;

        int amount = Random.Range(minLootAmount, maxLootAmount);

        for (int i = 0; i < amount; i++) {
            SpawnLoot();
        }

        opened = true;
        ChestOpened?.Invoke();
    }

    private void SpawnLoot() {
        GameObject lootObject = Instantiate(lootTable.GetPrefab(), transform.position, Quaternion.identity);

        Rigidbody2D rb;
        if(!lootObject.TryGetComponent<Rigidbody2D>(out rb)) {
            return;
        }

        rb.AddForce(new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized * awayPushSpeed);
    }
}
