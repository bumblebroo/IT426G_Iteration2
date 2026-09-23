using System.Collections;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [Space]

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

    private float timer = 0;

    [Space]

    [SerializeField]
    private float pickUpRadius;

    [SerializeField]
    private LayerMask pickUpLayers;

    [SerializeField]
    private GameObject dropPrefab;

    private GunPickup currentAvailablePickup;

    public Vector3 MouseWorldPosition => mouseWorldPosition;

    private void Start() {
        StartCoroutine(MoveCamera());

        gunSprite.sprite = null;
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
        int otherGunIndex = playerData.currentGunIndex == 0 ? 1 : 0;
        if (playerData.guns[otherGunIndex] == null) {
            return;
        }

        autoShoot = false;
        playerData.currentGunIndex = otherGunIndex;
        LoadGun();
    }

    private void LoadGun() {
        if (playerData.guns[playerData.currentGunIndex] == null) {
            Debug.Log("Missing gun", this);
            return;
        }

        gunSprite.sprite = playerData.guns[playerData.currentGunIndex].Sprite;
        firePoint.localPosition = new Vector2(playerData.guns[playerData.currentGunIndex].FirePointDistance, firePoint.localPosition.y);

        // play sound
    }

    public void PickUpGun(InputAction.CallbackContext context) {
        if (context.phase != InputActionPhase.Started) {
            return;
        }

        if (!currentAvailablePickup) {
            return;
        }

        if (!playerData.guns[playerData.currentGunIndex]) {
            playerData.guns[playerData.currentGunIndex] = currentAvailablePickup.PickUp();
            LoadGun();
            return;
        }

        int otherGun = playerData.currentGunIndex == 0 ? 1 : 0;
        if (playerData.guns[otherGun] == null) {
            playerData.guns[otherGun] = currentAvailablePickup.PickUp();
            return;
        }

        GameObject droppedGunGameObject = Instantiate(dropPrefab, transform.position, transform.rotation);
        GunPickup gunPickupComponent = droppedGunGameObject.GetComponent<GunPickup>();
        gunPickupComponent.Init(playerData.guns[playerData.currentGunIndex]);

        playerData.guns[playerData.currentGunIndex] = currentAvailablePickup.PickUp();
        LoadGun();
    }

    public void HandleShoot(InputAction.CallbackContext context) {
        if (!playerData.guns[playerData.currentGunIndex]) {
            return;
        }

        if (playerData.guns[playerData.currentGunIndex].IsFullAuto) {
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
        if (!playerData.guns[playerData.currentGunIndex]) {
            return;
        }

        if (!HasAmmo()) {
            return;
        }

        if (timer < 1 / playerData.guns[playerData.currentGunIndex].FireRate) {
            return;
        }

        for (int i = 0; i < playerData.guns[playerData.currentGunIndex].ShootEffectPrefabs.Length; i++) {
            Instantiate(playerData.guns[playerData.currentGunIndex].ShootEffectPrefabs[i], firePoint.position, firePoint.rotation);
        }

        for (int i = 0; i < playerData.guns[playerData.currentGunIndex].BulletAmount; i++) {
            GameObject projectileGameObject = Instantiate(playerData.guns[playerData.currentGunIndex].ProjectileScriptableObject.Prefab, firePoint.position, firePoint.rotation);

            float angle = Random.Range(-playerData.guns[playerData.currentGunIndex].AngleVariation, playerData.guns[playerData.currentGunIndex].AngleVariation);
            angle *= Mathf.Deg2Rad;
            projectileGameObject.transform.rotation *= new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));

            Projectile projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.Init(playerData.guns[playerData.currentGunIndex].ProjectileScriptableObject);
        }

        PlayerMovement.Instance.PlayerKnockBackState.KnockBack(playerData.guns[playerData.currentGunIndex].KnockBack * -firePoint.right);
        ReduceAmmo();
        timer = 0;
    }

    private bool HasAmmo() {
        switch (playerData.guns[playerData.currentGunIndex].AmmoType) {
            case AmmoEnum.Shell:
                return playerData.ShellCount > 0;
            case AmmoEnum.Bullet:
                return playerData.BulletCount > 0;
            case AmmoEnum.Bolt:
                return playerData.BoltCount > 0;
            case AmmoEnum.Bomb:
                return playerData.BombCount > 0;
            default:
                return false;
        }
    }

    private void ReduceAmmo() {
        switch (playerData.guns[playerData.currentGunIndex].AmmoType) {
            case AmmoEnum.Shell:
                playerData.ShellCount--;
                break;
            case AmmoEnum.Bullet:
                playerData.BulletCount--;
                break;
            case AmmoEnum.Bolt:
                playerData.BoltCount--;
                break;
            case AmmoEnum.Bomb:
                playerData.BombCount--;
                break;
            default:
                Debug.LogError("Missing enum implementation");
                break;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTarget.position, 0.1f);

        Gizmos.DrawWireSphere(transform.position, cameraMaxDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
