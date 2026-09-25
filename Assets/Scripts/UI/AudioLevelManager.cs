using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioLevelManager : MonoBehaviour
{
    [SerializeField]
    private Slider masterSlider;

    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private AudioMixer audioMixer;

    private void Start() {
        float masterVolume = PlayerPrefs.GetFloat("masterVolume", 0);
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0);

        SetMixerValue("masterVolume", masterVolume);
        SetMixerValue("sfxVolume", sfxVolume);
        SetMixerValue("musicVolume", musicVolume);

        masterSlider.value = masterVolume;
        sfxSlider.value = sfxVolume;
        musicSlider.value = musicVolume;
    }

    private void OnValidate() {
        if(masterSlider == null) {
            Debug.LogError("Missing master volume slider", this);
        }
        if (sfxSlider == null) {
            Debug.LogError("Missing sfx volume slider", this);
        }
        if (musicSlider == null) {
            Debug.LogError("Missing music volume slider", this);
        }
        if (audioMixer == null) {
            Debug.LogError("Missing audioMixer", this);
        }
    }

    private void SetMixerValue(string parameter, float value) {
        audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(parameter, value);
    }

    public void MasterSliderUpdateValue() {
        SetMixerValue("masterVolume", masterSlider.value);
    }

    public void SfxSliderUpdateValue() {
        SetMixerValue("sfxVolume", sfxSlider.value);
    }

    public void MusicSliderUpdateValue() {
        SetMixerValue("musicVolume", musicSlider.value);
    }
}
