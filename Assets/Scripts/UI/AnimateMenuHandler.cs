using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AnimateMenuHandler : MonoBehaviour
{
    [SerializeField]
    private RectTransform menuTransform;

    [SerializeField]
    private Vector2 showPos, hidePos;

    [Space]

    [SerializeField]
    private float animationTime;

    [SerializeField]
    private AnimationCurve animationCurve;

    private float lerpValue;

    private bool isHidden = true;

    private void Start() {
        isHidden = true;
        menuTransform.anchoredPosition = hidePos;
        menuTransform.gameObject.SetActive(false);
        lerpValue = 0;
    }

    public void ToggleMenu(InputAction.CallbackContext context) {
        if (context.phase != InputActionPhase.Started) {
            return;
        }

        ToggleMenu();
    }

    public void ToggleMenu () {
        StopAllCoroutines();
        StartCoroutine(AnimateMenu());
    }


    private IEnumerator AnimateMenu() {
        if (isHidden) {
            menuTransform.gameObject.SetActive(true);
            isHidden = false;
        } else {
            isHidden = true;
        }

        while (true) {
            lerpValue = Mathf.Clamp01(lerpValue + Time.deltaTime / animationTime * (isHidden ? -1 : 1));
            menuTransform.anchoredPosition = Vector2.LerpUnclamped(hidePos, showPos, animationCurve.Evaluate(lerpValue));

            if ((isHidden && lerpValue == 0) || (!isHidden && lerpValue == 1)) {
                break;
            }

            yield return new WaitForEndOfFrame();
        }


        if (isHidden) {
            menuTransform.gameObject.SetActive(false);
        }
    }
}
