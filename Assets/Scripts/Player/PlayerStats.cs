using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int health = 3;
    public int maxHealth = 3;
    public int healthRegen;

    [Header("Movement")]
    public float movementSpeed = 5.0f;
    public float dashRange = 10.0f;
    public float dashCooldown = 1.0f;

    void Awake()
    {
        GetComponent<PlayerController>().movementSpeed = movementSpeed;
        GetComponent<PlayerController>().dashRange = dashRange;
        GetComponent<PlayerController>().dashCooldown = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: give stats to Controller when changed with setter
    }

}
