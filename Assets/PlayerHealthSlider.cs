using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Slider mainHealthSlider;

    [SerializeField]
    private Slider delayeHealthSlider;

    [SerializeField]
    [Min(0)]
    private float delayLerpSpeed = 1f;

    private bool animatingDelaySlider = false;

    private void Start() {
        mainHealthSlider.maxValue = playerData.MaxHealth;
        delayeHealthSlider.maxValue = playerData.MaxHealth;

        mainHealthSlider.value = playerData.CurrentHealth;
        delayeHealthSlider.value = playerData.CurrentHealth;

        playerData.healthUpdated.AddListener(UpdateSlider);
    }

    private void OnDestroy() {
        playerData.healthUpdated.RemoveListener(UpdateSlider);
    }

    private void UpdateSlider() {
        mainHealthSlider.value = playerData.CurrentHealth;

        if(mainHealthSlider.value > delayeHealthSlider.value) {
            delayeHealthSlider.value = mainHealthSlider.value;
            return;
        }

        if (animatingDelaySlider) {
            return;
        }

        StartCoroutine(AnimateDelaySlider());
    }

    private IEnumerator AnimateDelaySlider() {
        animatingDelaySlider = true;
        while(delayeHealthSlider.value != mainHealthSlider.value) {
            delayeHealthSlider.value = delayeHealthSlider.value - delayLerpSpeed * Time.deltaTime;
            if(delayeHealthSlider.value < mainHealthSlider.value) {
                delayeHealthSlider.value = mainHealthSlider.value;
            }
            yield return new WaitForEndOfFrame();
        }
        animatingDelaySlider = false;
    }
}
