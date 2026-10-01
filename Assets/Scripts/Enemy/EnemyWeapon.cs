using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float angleVariation = 3f;

    [SerializeField]
    private GameObject projectile;

    private float lastShot = 0;

    public void NeutralAlign(float xDir) {
        if (xDir > 0) {
            pivot.localScale = new Vector3(1, -1, 1);
        } else if (xDir < 0) {
            pivot.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ShootAtPlayer () {
        LookAtPlayer();

        if (Time.time - lastShot < (1 / fireRate)) {
            return;
        }

        GameObject projectileGameObject = Instantiate(projectile, firePoint.position, firePoint.rotation);

        float angle = Random.Range(-angleVariation, angleVariation);
        angle *= Mathf.Deg2Rad;
        projectileGameObject.transform.rotation *= new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));
        lastShot = Time.time;
    }

    private void LookAtPlayer() {
        Vector2 gunDir = PlayerMovement.Instance.gameObject.transform.position - pivot.position;
        gunDir.Normalize();
        float angle = Mathf.Atan2(gunDir.y, gunDir.x);
        pivot.rotation = new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));

        if (transform.position.x > PlayerMovement.Instance.gameObject.transform.position.x) {
            pivot.localScale = new Vector3(1, -1, 1);
        } else if (transform.position.x < PlayerMovement.Instance.gameObject.transform.position.x) {
            pivot.localScale = new Vector3(1, 1, 1);
        }
    }
}
