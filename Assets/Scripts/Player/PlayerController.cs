using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidBody;
    private PlayerStats playerStats;
    private Collider2D playerCollider;

    [Header("Movement")]
    public Vector2 direction;
    private float dashTimer;
    private bool isDashing;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        playerCollider = GetComponentsInChildren<Collider2D>().FirstOrDefault(collider => collider.gameObject.layer == LayerMask.NameToLayer("PlayerCollision"));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            rigidBody.linearVelocity = new Vector2(direction.x * playerStats.GetMovementSpeed(), direction.y * playerStats.GetMovementSpeed());
        }
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        playerCollider.enabled = false;

        Vector2 dashDirection = direction.normalized;
        if (dashDirection == Vector2.zero)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            dashDirection = (mousePosition - (Vector2)transform.position).normalized;
        }

        rigidBody.AddForce(dashDirection * playerStats.GetDashRange(), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        dashTimer = playerStats.GetDashCooldown();
        playerCollider.enabled = true;
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
