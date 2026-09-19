using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "Scriptable Objects/ProjectileScriptableObject")]
public class ProjectileScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int pierceAmount;

    public GameObject Prefab => prefab;
    public float Damage => damage;
    public float Speed => speed;
    public int PierceAmount => pierceAmount;
}
