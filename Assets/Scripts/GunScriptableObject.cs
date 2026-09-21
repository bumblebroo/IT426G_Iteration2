using UnityEngine;

[CreateAssetMenu(fileName = "GunScriptableObject", menuName = "Scriptable Objects/GunScriptableObject")]
public class GunScriptableObject : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    [Tooltip("Bullets per second")]
    [Min(0)]
    private float fireRate;

    [SerializeField]
    private bool isFullAuto;

    [SerializeField]
    [Min(0)]
    private float angleVariation;

    [SerializeField]
    [Min(1)]
    private int bulletAmount = 1;

    [SerializeField]
    [Min(0)]
    private float firePointDistance;

    [SerializeField]
    private float knockBack = 0;


    [SerializeField]
    private ProjectileScriptableObject projectileScriptableObject;

    [SerializeField]
    private GameObject[] shootEffectPrefabs;

    public Sprite Sprite => sprite;
    public float FireRate => fireRate;
    public bool IsFullAuto => isFullAuto;
    public float AngleVariation => angleVariation;
    public int BulletAmount => bulletAmount;
    public float FirePointDistance => firePointDistance;
    public float KnockBack => knockBack;

    public ProjectileScriptableObject ProjectileScriptableObject => projectileScriptableObject;
    public GameObject[] ShootEffectPrefabs => shootEffectPrefabs;

    private void OnValidate() {
        if (!sprite) {
            Debug.LogError("Missing sprite", this);
        }

        if (!projectileScriptableObject) {
            Debug.LogError("Missing projectile", this);
        }
    }
}
