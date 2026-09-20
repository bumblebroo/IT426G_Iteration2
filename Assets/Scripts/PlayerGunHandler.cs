using System.Collections;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
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

    private bool autoShoot = false;


    [SerializeField]
    private GunScriptableObject startingGun;

    private GunScriptableObject[] guns;

    private int currentGunIndex;

    private float timer = 0;

    [Space]

    [SerializeField]
    private float pickUpRadius;

    [SerializeField]
    private LayerMask pickUpLayers;

    [SerializeField]
    private GameObject dropPrefab;

    private GunPickup currentAvailablePickup;


    private void Start() {
        StartCoroutine(MoveCamera());

        guns = new GunScriptableObject[2];

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
                gunPivot.localScale = new Vector3(1, -1, 1);
            } else if(playerSprite.transform.position.x < mouseWorldPosition.x) {
                playerSprite.flipX = false;
                gunPivot.localScale = new Vector3(1, 1, 1);
            }

            Vector2 gunDir = mouseWorldPosition - (Vector2)gunPivot.position;
            gunDir.Normalize();
            float angle = Mathf.Atan2(gunDir.y, gunDir.x);
            gunPivot.rotation = new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));

            timer += Time.deltaTime;

            if (autoShoot) {
                Shoot();
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, pickUpLayers);

            if (currentAvailablePickup) {
                currentAvailablePickup.DisableHighLight();
                currentAvailablePickup = null;
            }

            float shortestDist = float.PositiveInfinity;
            for (int i = 0; i < colliders.Length; i++) {
                GunPickup gunPickupComponent;
                if (!colliders[i].gameObject.TryGetComponent<GunPickup>(out gunPickupComponent)) {
                    continue;
                }

                float dist = Vector2.Distance(transform.position, colliders[i].transform.position);
                if(shortestDist < dist) {
                    continue;
                }

                currentAvailablePickup = gunPickupComponent;
                currentAvailablePickup.EnableHighlight();
                shortestDist = dist;
            }

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

    public void PickUpGun(InputAction.CallbackContext context) {
        if (context.phase != InputActionPhase.Started) {
            return;
        }

        if (!currentAvailablePickup) {
            return;
        }

        int otherGun = currentGunIndex == 0 ? 1 : 0;
        if (guns[otherGun] == null) {
            guns[otherGun] = currentAvailablePickup.PickUp();
            return;
        }

        GameObject droppedGunGameObject = Instantiate(dropPrefab, transform.position, transform.rotation);
        GunPickup gunPickupComponent = droppedGunGameObject.GetComponent<GunPickup>();
        gunPickupComponent.Init(guns[currentGunIndex]);

        guns[currentGunIndex] = currentAvailablePickup.PickUp();
        LoadGun();
    }

    public void HandleShoot(InputAction.CallbackContext context) {
        if (guns[currentGunIndex].IsFullAuto) {
            if(context.phase == InputActionPhase.Canceled) {
                autoShoot = false;
                return;
            }
            autoShoot = true;
            return;
        } else if(context.phase != InputActionPhase.Started) {
            return;
        }

        Shoot();
    }
    private void Shoot() {
        if(timer < 1 / guns[currentGunIndex].FireRate) {
            return;
        }

        Instantiate(guns[currentGunIndex].ShootEffectPrefab, firePoint.position, firePoint.rotation);

        for (int i = 0; i < guns[currentGunIndex].BulletAmount; i++) {
            GameObject projectileGameObject = Instantiate(guns[currentGunIndex].ProjectileScriptableObject.Prefab, firePoint.position, firePoint.rotation);

            float angle = Random.Range(-guns[currentGunIndex].AngleVariation, guns[currentGunIndex].AngleVariation);
            angle *= Mathf.Deg2Rad;
            projectileGameObject.transform.rotation *= new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));

            Projectile projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.Init(guns[currentGunIndex].ProjectileScriptableObject);
        }

        timer = 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTarget.position, 0.1f);

        Gizmos.DrawWireSphere(transform.position, cameraMaxDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
