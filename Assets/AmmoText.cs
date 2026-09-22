using TMPro;
using UnityEngine;

public class AmmoText : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private AmmoEnum ammoType;

    private void Start() {
        switch (ammoType) {
            case AmmoEnum.Shell:
                text.text = $"{playerData.ShellCount}";
                playerData.shellCountUpdated.AddListener(UpdateText);
                break;
            case AmmoEnum.Bullet:
                text.text = $"{playerData.BulletCount}";
                playerData.bulletCountUpdated.AddListener(UpdateText);
                break;
            case AmmoEnum.Bolt:
                text.text = $"{playerData.BoltCount}";
                playerData.boltCountUpdated.AddListener(UpdateText);
                break;
            case AmmoEnum.Bomb:
                text.text = $"{playerData.BombCount}";
                playerData.bombCountUpdated.AddListener(UpdateText);
                break;
            default:
                Debug.LogError("Missing enum implementation");
                break;
        }
    }
    
    private void UpdateText(int amount) {
        text.text = $"{amount}";
    }
}
