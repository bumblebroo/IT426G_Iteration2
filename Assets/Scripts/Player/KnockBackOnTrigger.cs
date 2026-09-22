using UnityEngine;

public class KnockBackOnTrigger : MonoBehaviour
{
    [SerializeField]
    private float force;

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerKnockBackState playerKnockBackState;
        if(collision.gameObject.TryGetComponent<PlayerKnockBackState>(out playerKnockBackState)) {
            playerKnockBackState.KnockBack((collision.gameObject.transform.position - transform.position).normalized * force);
        }
    }
}
