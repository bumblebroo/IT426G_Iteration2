using UnityEngine;


public class Projectile : MonoBehaviour
{
    private ProjectileScriptableObject scriptableObject;

    [SerializeField]
    private Rigidbody2D rb;

    private int amountHit = 0;
    
    public void Init(ProjectileScriptableObject scriptableObject) {
        this.scriptableObject = scriptableObject;

        rb.AddForce(transform.right * scriptableObject.Speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        EnemyHealth enemy;
        if(!collision.gameObject.TryGetComponent<EnemyHealth>(out enemy)) {
            Destroy(this.gameObject);
            return;
        }

        enemy.TakeDamage(scriptableObject.Damage);

        amountHit += 1;

        if(amountHit >= scriptableObject.PierceAmount) {
            Destroy(this.gameObject);
        }
    }
}