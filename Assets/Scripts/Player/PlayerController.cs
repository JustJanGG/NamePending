using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;

    [Header("Movement")]
    public Vector2 direction;
    public float movementSpeed;

    public float dashRange;
    public float dashCooldown;
    private float dashTimer;
    private bool isDashing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isDashing = false;
    }

    // Update is called once per frame
    private void Update()
    {
        dashTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isDashing)
        {
            rigidBody.linearVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
        }
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        Vector2 dashDirection = direction.normalized;
        if (dashDirection == Vector2.zero)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            dashDirection = (mousePosition - (Vector2)transform.position).normalized;
        }

        rigidBody.AddForce(dashDirection * dashRange, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        dashTimer = dashCooldown;
        isDashing = false;
    }

    public void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>().normalized;
    }

    public void HandleDashInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && dashTimer <= 0)
        {
            StartCoroutine(Dash());
        }
    }

}
