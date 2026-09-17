using System.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHandler : MonoBehaviour
{

    [SerializeField]
    private float cameraMaxDistance;

    [SerializeField]
    [Min(0)]
    [Range(0,1)]
    private float percentDistance;

    [SerializeField]
    private Transform cameraTarget;

    private Vector2 mousePosition;

    private void Start() {
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera() {
        while (true) {
            Vector2 mouseDir = mousePosition - (Vector2)transform.position;
            mouseDir *= percentDistance;

            Vector2 targetPos = mouseDir;
            if(mouseDir.magnitude > cameraMaxDistance) {
                mouseDir.Normalize();

                targetPos = mouseDir * cameraMaxDistance;
            }

            cameraTarget.position = transform.position + (Vector3)targetPos;

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context) {
        mousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTarget.position, 0.1f);
    }
}
