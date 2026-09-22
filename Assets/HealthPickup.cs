using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private int amount = 1;

    private bool didPickup = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (didPickup) {
            return;
        }
        
        if (!collision.gameObject.GetComponent<PlayerMovement>()) {
            return;
        }

        playerData.CurrentHealth += amount;
        didPickup = true;

        Destroy(this.gameObject);
    }
}
