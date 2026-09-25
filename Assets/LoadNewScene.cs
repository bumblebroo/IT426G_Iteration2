using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private float fadeTime;

    public void LoadScene(string name) {
        StartCoroutine(FadeIn(name));
    }


    public void LoadScene(int index) {
        StartCoroutine(FadeIn(index));
    }

    private IEnumerator FadeIn(string name) {
        image.gameObject.SetActive(true);
        float time = 0;
        Color startColor = Color.black - Color.black;
        Color targetColor = Color.black;

        while (time <= 1) {
            image.color = Color.Lerp(startColor, targetColor, time);
            time += Time.deltaTime / fadeTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(name);
    }


    private IEnumerator FadeIn(int index) {
        image.gameObject.SetActive(true);
        float time = 0;
        Color startColor = Color.black - Color.black;
        Color targetColor = Color.black;

        while (time <= 1) {
            image.color = Color.Lerp(startColor, targetColor, time);
            time += Time.deltaTime / fadeTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(index);
    }
}
