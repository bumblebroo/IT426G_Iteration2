using UnityEngine;

public class ResetPlayerData : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    void Start()
    {
        playerData.ResetData();
    }
}
