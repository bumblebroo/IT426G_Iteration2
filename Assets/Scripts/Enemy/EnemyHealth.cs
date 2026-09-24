using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float startHealth;

    private float currentHealth;

    public float StartHealth => startHealth;
    public float CurrentHealth => currentHealth;

    private void Start() {
        currentHealth = startHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if(currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }
}
