using UnityEngine;

public class ResetPlayerData : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    void Awake()
    {
        playerData.ResetData();
    }
}
