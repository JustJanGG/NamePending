using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int health;
    public int maxHealth;
    public int healthRegen;

    [Header("Movement")]
    public float movementSpeed;
    public float dashRange;
    public float dashCooldown;

    [Header("Damage")]
    public float baseDamage;

    [Header("Resistances")]
    [Range(0, 1)]
    public float physicalResistance;
    [Range(0, 1)]
    public float fireResistance;
    [Range(0, 1)]
    public float coldResistance;
    [Range(0, 1)]
    public float lightningResistance;
    private Dictionary<DamageType, float> resistances = new();

    private void Start()
    {
        resistances.Add(DamageType.Physical, physicalResistance);
        resistances.Add(DamageType.Fire, fireResistance);
        resistances.Add(DamageType.Cold, coldResistance);
        resistances.Add(DamageType.Lightning, lightningResistance);
    }

    public void TakeDamage(Dictionary<DamageType, float> damage)
    {
        foreach (var item in damage)
        {
            int damageTaken = (int)(damage[item.Key] * (1 - resistances[item.Key]));
            health -= damageTaken;
            if (health <= 0)
            {
                GameManager.instance.GameOver();
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
    }
}
