using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    [SerializeField]
    private int shellCount = 0;

    [SerializeField]
    private int bulletCount = 0;

    [SerializeField]
    private int boltCount = 0;

    [SerializeField]
    private int bombCount = 0;

    [HideInInspector]
    public UnityEvent healthUpdated;

    [HideInInspector]
    public UnityEvent<int> shellCountUpdated;
    [HideInInspector]
    public UnityEvent<int> bulletCountUpdated;
    [HideInInspector]
    public UnityEvent<int> boltCountUpdated;
    [HideInInspector]
    public UnityEvent<int> bombCountUpdated;

    [HideInInspector]
    public GunScriptableObject[] guns = new GunScriptableObject[2];

    [HideInInspector]
    public int currentGunIndex;

    public int MaxHealth => maxHealth;

    public int CurrentHealth {
        get {
            return currentHealth;
        }
        set {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            healthUpdated?.Invoke();
        }
    }

    public int ShellCount {
        get {
            return shellCount;
        }
        set {
            shellCount = Mathf.Min(value, 0);
            shellCountUpdated?.Invoke(shellCount);
        }
    }

    public int BulletCount {
        get {
            return bulletCount;
        }
        set {
            bulletCount = Mathf.Min(value, 0);
            bulletCountUpdated?.Invoke(bulletCount);
        }
    }

    public int BoltCount {
        get {
            return boltCount;
        }
        set {
            boltCount = Mathf.Min(value, 0);
            boltCountUpdated?.Invoke(boltCount);
        }
    }

    public int BombCount {
        get {
            return bombCount;
        }
        set {
            bombCount = Mathf.Min(value, 0);
            bombCountUpdated?.Invoke(bombCount);
        }
    }

    public void ResetData() {
        CurrentHealth = maxHealth;
        shellCount = 0;
        bulletCount = 0;
        boltCount = 0;
        bombCount = 0;

        guns = new GunScriptableObject[2];
        currentGunIndex = 0;
    }
}
