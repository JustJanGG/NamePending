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

}
