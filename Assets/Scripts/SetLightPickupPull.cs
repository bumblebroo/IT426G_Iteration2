using UnityEngine;

public class SetLightPickupPull : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        LightPickup lightPickup;
        if(!collision.gameObject.TryGetComponent<LightPickup>(out lightPickup)) {
            return;
        }

        lightPickup.SetTarget(this.transform);
    }
}
