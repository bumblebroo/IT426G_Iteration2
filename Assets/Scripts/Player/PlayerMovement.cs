using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [Space]

    [SerializeField]
    private PlayerWalkState playerWalkState;
    [SerializeField]
    private PlayerDashState playerDashState;
    [SerializeField]
    private PlayerKnockBackState playerKnockBackState;

    private PlayerMovementState currentMovementState;

    private Vector2 desiredDirection = Vector2.zero;

    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;

    public PlayerWalkState PlayerWalkState => playerWalkState;
    public PlayerDashState PlayerDashState => playerDashState;
    public PlayerKnockBackState PlayerKnockBackState => playerKnockBackState;

    private void OnValidate() {
        if (!animator) {
            Debug.LogError("Missing animator");
        }

        playerWalkState.OnValidate(this.gameObject);
        playerDashState.OnValidate(this.gameObject);
        playerKnockBackState.OnValidate(this.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerWalkState.Initialize(playerData, this);
        playerDashState.Initialize(playerData, this);
        playerKnockBackState.Initialize(playerData, this);

        currentMovementState = playerWalkState;

        StartCoroutine(DoMovement());
    }

    private IEnumerator DoMovement() {
        while (true) {
            yield return new WaitForEndOfFrame();

            if (currentMovementState == null) {
                Debug.LogError("Current movement state is null?", this);
                continue;
            }

            currentMovementState.MovementUpdate(rb, desiredDirection);
        }
    }

    public void Transition(PlayerMovementState newState) {
        if(newState == null) {
            Debug.Log($"Transition from {currentMovementState} not allowed as no new state was supplied!", this);
            return;
        }

        currentMovementState.ExitState();
        currentMovementState = newState;
        currentMovementState.EnterState(animator);
    }

    public void TakeMovementInput(InputAction.CallbackContext context) {
        desiredDirection = context.ReadValue<Vector2>();
    }
}
