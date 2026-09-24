using UnityEngine;

public class PlayerHealth : HealthBase
{
    [SerializeField]
    private PlayerData playerData;
    public override void TakeDamage(int damage) {
        playerData.CurrentHealth -= damage;
    }
}
