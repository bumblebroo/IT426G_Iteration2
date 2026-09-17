using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHandler : MonoBehaviour
{

    [SerializeField]
    private float cameraDistance;

    [SerializeField]
    private Transform cameraTarget;

    private Vector2 mousePosition;

    private void Start() {
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera() {
        while (true) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 mouseDir = mousePos - (Vector2)transform.position;

            Vector2 targetPos = mouseDir;
            if(mouseDir.magnitude > cameraDistance) {
                mouseDir.Normalize();

                targetPos = mouseDir * cameraDistance;
            }

            cameraTarget.position = transform.position + (Vector3)targetPos;

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context) {
        Debug.Log("moved mouse");
        mousePosition = context.ReadValue<Vector2>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTarget.position, 0.1f);
    }
}
