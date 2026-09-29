using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    public void Start() {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerHealth player;
        if (!collision.gameObject.TryGetComponent<PlayerHealth>(out player)) {
            Destroy(this.gameObject);
            return;
        }

        player.TakeDamage(damage);

        Destroy(this.gameObject);
    }
}
