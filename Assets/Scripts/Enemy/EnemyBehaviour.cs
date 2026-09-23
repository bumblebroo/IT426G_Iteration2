using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private EnemyState currentBehaviour;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Rigidbody2D rb;

    [Space]

    [SerializeField]
    private LayerMask lookLayers;

    [SerializeField]
    private float attackRange;

    protected bool CanSeePlayer {
        get {
            return Physics2D.Linecast(transform.position, PlayerMovement.Instance.transform.position, lookLayers);
        }
    }

    protected bool CanAttack {
        get {
            return Vector2.Distance(transform.position, PlayerMovement.Instance.transform.position) <= attackRange;
        }
    }

    protected virtual void Start()
    {
        currentBehaviour.Initialize(this, animator, rb, sr);
        currentBehaviour.EnterState();
    }

    protected virtual void Update()
    {
        currentBehaviour.EnemyUpdate();
    }

    protected virtual void Transition(EnemyState nextEnemyState) {
        currentBehaviour.ExitState();
        currentBehaviour = nextEnemyState;
        currentBehaviour.EnterState();
    }
}
