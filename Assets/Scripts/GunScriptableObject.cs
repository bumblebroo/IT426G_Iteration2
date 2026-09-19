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
    private float firePointDistance;


    [SerializeField]
    private ProjectileScriptableObject projectileScriptableObject;

    [SerializeField]
    private GameObject shootEffectPrefab;

    public Sprite Sprite => sprite;
    public float FireRate => fireRate;
    public bool IsFullAuto => isFullAuto;
    public float FirePointDistance => FirePointDistance;

    public ProjectileScriptableObject ProjectileScriptableObject => projectileScriptableObject;
    public GameObject ShootEffectPrefab => shootEffectPrefab;

    private void OnValidate() {
        if (!sprite) {
            Debug.LogError("Missing sprite", this);
        }

        if (!projectileScriptableObject) {
            Debug.LogError("Missing projectile", this);
        }

        if (!shootEffectPrefab) {
            Debug.LogError("Missing effects prefab", this);
        }
    }
}
