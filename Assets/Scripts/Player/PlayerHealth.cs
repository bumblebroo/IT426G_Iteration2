using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : HealthBase
{
    [SerializeField]
    private PlayerData playerData;

    public UnityEvent OnHit;
    public override void TakeDamage(int damage) {
        playerData.CurrentHealth -= damage;
    }
}
