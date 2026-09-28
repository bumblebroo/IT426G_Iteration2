using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField]
    private float hitRadius;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float knockBack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Vector2.Distance(transform.position, PlayerMovement.Instance.transform.position) <= hitRadius) {
            PlayerMovement.Instance.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            PlayerMovement.Instance.PlayerKnockBackState.KnockBack(knockBack * (PlayerMovement.Instance.transform.position - transform.position).normalized);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
