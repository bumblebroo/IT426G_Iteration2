using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutOnStart : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private float fadeTime;

    private void Start() {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        float time = 0;
        Color startColor = Color.black;
        Color targetColor = Color.black - Color.black;

        while (time <= 1) {
            image.color = Color.Lerp(startColor, targetColor, time);
            time += Time.deltaTime / fadeTime;
            yield return new WaitForEndOfFrame();
        }

        image.gameObject.SetActive(false);
    }
}
