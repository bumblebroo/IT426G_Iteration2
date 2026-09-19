using System.Collections;
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
    private Transform cameraTarget, gunPivot, firePoint;

    private Vector2 mouseScreenPosition;

    private Vector2 mouseWorldPosition;


    [SerializeField]
    private GunScriptableObject startingGun;

    private GunScriptableObject[] guns;

    private int currentGunIndex;

    private float timer = 0;


    private void Start() {
        StartCoroutine(MoveCamera());

        guns[0] = startingGun;
        currentGunIndex = 0;
        LoadGun();
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
                gunSprite.flipY = true;
            } else if(playerSprite.transform.position.x < mouseWorldPosition.x) {
                playerSprite.flipX = false;
                gunSprite.flipY = false;
            }

            Vector2 gunDir = mouseWorldPosition - (Vector2)gunPivot.position;
            gunDir.Normalize();
            float angle = Mathf.Atan2(gunDir.y, gunDir.x);
            gunPivot.rotation = new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));

            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context) {
        mouseScreenPosition = context.ReadValue<Vector2>();
    }

    public void SwitchGun(InputAction.CallbackContext context) {
        if(context.phase != InputActionPhase.Started) {
            return;
        }

        SwitchGun();
    }

    public void SwitchGun() {
        int otherGunIndex = currentGunIndex == 0 ? 1 : 0;
        if (guns[otherGunIndex] == null) {
            return;
        }

        currentGunIndex = otherGunIndex;
        LoadGun();
    }

    private void LoadGun() {
        if (guns[currentGunIndex] == null) {
            Debug.Log("Missing gun", this);
            return;
        }

        gunSprite.sprite = guns[currentGunIndex].Sprite;
        firePoint.localPosition = new Vector2(guns[currentGunIndex].FirePointDistance, firePoint.localPosition.y);

        // play sound
    }

    private void PickUpGun() {

    }

    public void HandleShoot(InputAction.CallbackContext context) {
        if (!guns[currentGunIndex].IsFullAuto && context.phase != InputActionPhase.Started) {
            return;
        }

        Shoot();
    }
    private void Shoot() {
        if(timer < 1 / guns[currentGunIndex].FireRate) {
            return;
        }

        GameObject projectileGameObject = Instantiate(guns[currentGunIndex].ProjectileScriptableObject.Prefab, firePoint.position, firePoint.rotation);

        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.Init(guns[currentGunIndex].ProjectileScriptableObject);


        timer = 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTarget.position, 0.1f);

        Gizmos.DrawWireSphere(transform.position, cameraMaxDistance);
    }
}
