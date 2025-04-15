using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int health = 3;
    public int maxHealth = 3;
    public int healthRegen;

    [Header("Movement")]
    public float movementSpeed = 5.0f;
    public float dashRange = 10.0f;
    public float dashCooldown = 1.0f;

    [Header("Damage")]
    public float fireDamage = 5.0f;

}
