using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;

    [Header("Movement")]
    private Vector2 direction;
    public float movementSpeed = 9f;
    public float acceleration = 7f;
    public float decceleration = 9f;
    public float velPower = 1.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveRight();
        MoveUp();
    }
    private void MoveRight()
    {
        float targetSpeed = direction.x * movementSpeed;
        float speedDif = targetSpeed - rigidBody.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rigidBody.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void MoveUp()
    {
        float targetSpeed = direction.y * movementSpeed;
        float speedDif = targetSpeed - rigidBody.linearVelocity.y;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rigidBody.AddForce(movement * Vector2.up, ForceMode2D.Force);
    }


    public void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }
}
