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

    void Start()
    {
        isDashing = false;
    }

    private void Update()
    {
        if (dashTimer > -1.0f)
        {
            dashTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isDashing)
        {
            rigidBody.linearVelocity = new Vector2(direction.x * playerStats.movementSpeed, direction.y * playerStats.movementSpeed);
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        playerCollider.excludeLayers = LayerMask.GetMask("EnemyCollision");

        Vector2 dashDirection = direction.normalized;
        if (dashDirection == Vector2.zero)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            dashDirection = (mousePosition - (Vector2)transform.position).normalized;
        }

        rigidBody.AddForce(dashDirection * playerStats.dashRange, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        dashTimer = playerStats.dashCooldown;
        playerCollider.excludeLayers = LayerMask.GetMask("Nothing");
        isDashing = false;
    }

    public void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>().normalized;
        if (GameManager.instance.gameState == GameState.Inventory)
        {
            GameManager.instance.SetGameState(GameState.InGame);
        }
    }

    public void HandleDashInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && dashTimer <= 0 && GameManager.instance.gameState == GameState.InGame)
        {
            StartCoroutine(Dash());
        }
    }

}
