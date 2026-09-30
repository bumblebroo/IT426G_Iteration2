using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    [Space]

    [SerializeField]
    private LootTable lootTable;

    [Space]

    public UnityEvent OnHit;

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

        OnHit?.Invoke();

        if(currentHealth <= 0) {
            if (lootTable) {
                Instantiate(lootTable.GetPrefab(), transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    private IEnumerator flash() {
        sr.material = hitflashMaterial;
        yield return new WaitForSeconds(flashDuration);
        sr.material = currentMaterial;
    }
}
