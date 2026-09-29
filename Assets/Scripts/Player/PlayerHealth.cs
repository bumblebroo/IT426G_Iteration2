using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : HealthBase
{
    [SerializeField]
    private PlayerData playerData;

    public UnityEvent OnHit;

    public UnityEvent OnDeath;
    public override void TakeDamage(int damage) {
        playerData.CurrentHealth -= damage;
    
        if(playerData.CurrentHealth <= 0) {
            OnDeath?.Invoke();
        }
    }
}
