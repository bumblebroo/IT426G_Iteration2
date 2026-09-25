using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHurtEffect : MonoBehaviour
{
    [SerializeField]
    private Volume targetVolume;

    [SerializeField]
    private float weightDecayTime;

    // Update is called once per frame
    void Update()
    {
        if(targetVolume.weight > 0) {
            targetVolume.weight = Mathf.Clamp01(targetVolume.weight - Time.deltaTime / weightDecayTime);
        }
    }

    public void AddVolumeWeight(float amount) {
        targetVolume.weight = Mathf.Clamp01(targetVolume.weight + amount);
    }
}
