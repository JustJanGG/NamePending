using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    
    [Header("Movement")]
    public Vector2 direction;
    private float movementSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        movementSpeed = GetComponent<PlayerStats>().movementSpeed;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidBody.linearVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
    }

    public void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>().normalized;
    }
}
