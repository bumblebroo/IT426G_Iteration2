using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemyState currentState;

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

    [SerializeField]
    private float playerPosMargin;

    private Vector2 playerPosKnowledge;

    public Animator Animator => animator;
    public SpriteRenderer Sr => sr;
    public Rigidbody2D Rb => rb;

    public bool CanSeePlayer {
        get {
            return !Physics2D.Linecast(transform.position, PlayerMovement.Instance.transform.position, lookLayers);
        }
    }

    public bool CanFindPlayer {
        get {
            if (CanSeePlayer) {
                playerPosKnowledge = PlayerMovement.Instance.transform.position;
                return true;
            }

            if(Vector2.Distance(transform.position, playerPosKnowledge) > playerPosMargin) {
                return true;
            }

            return false;
        }
    }

    public Vector2 PlayerPosKnowledge => playerPosKnowledge;

    public bool CanAttack {
        get {
            return Vector2.Distance(transform.position, PlayerMovement.Instance.transform.position) <= attackRange;
        }
    }

    protected virtual void Start()
    {       
        currentState.EnterState();
    }

    protected virtual void Update()
    {
        HandleTransitions();
        currentState.EnemyUpdate();
    }

    protected abstract void HandleTransitions();

    protected virtual void Transition(EnemyState nextEnemyState) {
        currentState.ExitState();
        currentState = nextEnemyState;
        currentState.EnterState();
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
