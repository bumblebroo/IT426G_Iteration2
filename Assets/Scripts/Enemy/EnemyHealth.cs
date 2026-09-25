using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float startHealth;

    private float currentHealth;

    [SerializeField]
    private Material hitflashMaterial;

    private Material currentMaterial;

    [SerializeField]
    private float flashDuration;

    [SerializeField]
    private SpriteRenderer sr;

    public float StartHealth => startHealth;
    public float CurrentHealth => currentHealth;

    private void Start() {
        currentHealth = startHealth;
        currentMaterial = sr.material;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        StartCoroutine(flash());

        if(currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator flash() {
        sr.material = hitflashMaterial;
        yield return new WaitForSeconds(flashDuration);
        sr.material = currentMaterial;
    }
}
