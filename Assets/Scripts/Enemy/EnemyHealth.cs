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
    private float flashDuration = 0.05f;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private GameObject hurtParticlePrefab;

    public float StartHealth => startHealth;
    public float CurrentHealth => currentHealth;

    private void Start() {
        currentHealth = startHealth;
        currentMaterial = sr.material;
    }

    public void TakeDamage(float damage, GameObject source) {
        currentHealth -= damage;

        if (hurtParticlePrefab) {
            Vector2 dir = source.transform.position - transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            GameObject particles = Instantiate(hurtParticlePrefab, transform.position, Quaternion.identity);
            particles.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
        }

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
