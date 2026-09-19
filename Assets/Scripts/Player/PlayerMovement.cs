using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerWalkState), typeof(PlayerDashState), typeof(PlayerKnockBackState))]
public class PlayerMovement : MonoBehaviour
{
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerWalkState.Initialize(this, animator, rb);
        playerDashState.Initialize(this, animator, rb);
        playerKnockBackState.Initialize(this, animator, rb);

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

            currentMovementState.MovementUpdate(desiredDirection);
        }
    }

    public void Transition(PlayerMovementState newState) {
        if(newState == null) {
            Debug.Log($"Transition from {currentMovementState} not allowed as no new state was supplied!", this);
            return;
        }

        if(newState == playerDashState && !playerDashState.CanDash) {
            return;
        }

        currentMovementState.ExitState();
        currentMovementState = newState;
        currentMovementState.EnterState();
    }

    public void TakeMovementInput(InputAction.CallbackContext context) {
        desiredDirection = context.ReadValue<Vector2>();
    }

    public void Dash(InputAction.CallbackContext context) {
        if(context.phase != InputActionPhase.Started) {
            return;
        }

        if(currentMovementState == playerKnockBackState) {
            return;
        }

        Transition(playerDashState);
    }
}