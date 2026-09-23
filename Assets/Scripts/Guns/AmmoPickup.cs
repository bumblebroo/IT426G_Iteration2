using UnityEngine;

public class AmmoPickup : LightPickup
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private AmmoEnum ammoType;

    [SerializeField]
    private int amount;

    private bool didPickup = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.GetComponent<PlayerMovement>()) {
            return;
        }

        if (didPickup) {
            Destroy(this.gameObject);
            return;
        }

        didPickup = true;

        switch (ammoType) {
            case AmmoEnum.Shell:
                playerData.ShellCount += amount;
                break;
            case AmmoEnum.Bullet:
                playerData.BulletCount += amount;
                break;
            case AmmoEnum.Bolt:
                playerData.BoltCount += amount;
                break;
            case AmmoEnum.Bomb:
                playerData.BombCount += amount;
                break;
            default:
                Debug.LogError("Missing enum implementation");
                break;
        }

        Destroy(this.gameObject);
    }
}
