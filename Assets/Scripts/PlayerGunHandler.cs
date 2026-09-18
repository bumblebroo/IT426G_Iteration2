using System.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer playerSprite;
    [SerializeField]
    private SpriteRenderer gunSprite;


    [SerializeField]
    private float cameraMaxDistance;

    [SerializeField]
    [Min(0)]
    [Range(0,1)]
    private float percentDistance;

    [SerializeField]
    private Transform cameraTarget, gunPivot;

    private Vector2 mouseScreenPosition;

    private Vector2 mouseWorldPosition;

    private void Start() {
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera() {
        while (true) {
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 mouseDir = mouseWorldPosition - (Vector2)transform.position;
            mouseDir *= percentDistance;

            Vector2 targetPos = mouseDir;
            if(mouseDir.magnitude > cameraMaxDistance) {
                mouseDir.Normalize();

                targetPos = mouseDir * cameraMaxDistance;
            }

            cameraTarget.position = transform.position + (Vector3)targetPos;

            if(playerSprite.transform.position.x > mouseWorldPosition.x) {
                playerSprite.flipX = true;
            } else if(playerSprite.transform.position.x < mouseWorldPosition.x) {
                playerSprite.flipX = false;
            }

            Vector2 gunDir = (Vector2)mouseWorldPosition - (Vector2)gunPivot.position;
            gunDir.Normalize();
            float angle = Mathf.Atan2(gunDir.y, gunDir.x);
            gunPivot.rotation = new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));

            if (gunDir.x > 0) {
                gunSprite.flipY = false;
                Debug.Log("mouse is to the right");
            } else if (gunDir.x < 0) {
                Debug.Log("mouse is to the left");
                gunSprite.flipY = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context) {
        mouseScreenPosition = context.ReadValue<Vector2>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTarget.position, 0.1f);

        Gizmos.DrawWireSphere(transform.position, cameraMaxDistance);
    }
}
