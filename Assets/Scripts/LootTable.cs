using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField]
    private LootInfo[] lootInfos;

    public LootInfo[] LootInfos => lootInfos;


    public GameObject GetPrefab() {
        if(lootInfos.Length == 0) {
            Debug.LogError("Missing lootinfos");
            return null;
        }

        GameObject prefab = null;

        float total = lootInfos.Select(a => a.Probability).Sum();
        float select = Random.Range(0, total);

        float lastMax = 0;
        for (int i = 0; i < lootInfos.Length; i++) {
            lastMax += lootInfos[i].Probability;
            if(select > lastMax) {
                continue;
            }

            prefab = lootInfos[i].Prefab;
            break;
        }

        return prefab;
    }
}

[System.Serializable]
public struct LootInfo {
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float probability;

    public GameObject Prefab => prefab;

    public float Probability => probability;
}