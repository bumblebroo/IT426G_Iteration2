using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField]
    GunScriptableObject scriptableObject;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private GameObject pickUpEffect;

    [SerializeField]
    private GameObject pickUpHighlighter;

    private void Start() {
        if (scriptableObject) {
            sr.sprite = scriptableObject.Sprite;
        }
    }

    public void Init(GunScriptableObject scriptableObject) {
        this.scriptableObject = scriptableObject;
        sr.sprite = scriptableObject.Sprite;
    }

    public void EnableHighlight() {
        pickUpHighlighter.SetActive(true);
    }

    public void DisableHighLight() {
        pickUpHighlighter.SetActive(false);
    }

    public GunScriptableObject PickUp() {
        Destroy(this.gameObject);

        Instantiate(pickUpEffect, transform.position, transform.rotation);

        return scriptableObject;
    }
}
