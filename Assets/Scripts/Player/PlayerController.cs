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
    private Animator animator;

    [Header("Movement")]
    public Vector2 direction;
    private float dashTimer;
    private bool isDashing;
    private Vector2 lastMoveDirection;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        playerCollider = GetComponentsInChildren<Collider2D>().FirstOrDefault(collider => collider.gameObject.layer == LayerMask.NameToLayer("PlayerCollision"));
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        isDashing = false;
        lastMoveDirection = Vector2.right;
    }

    private void Update()
    {
        animator.SetFloat("XSpeed", direction.x);
        animator.SetFloat("YSpeed", direction.y);
        animator.SetFloat("velocity", direction.magnitude);

        if (direction.magnitude > 0.01f)
        {
            lastMoveDirection = direction;
            animator.SetFloat("lastXSpeed", lastMoveDirection.x);
            animator.SetFloat("lastYSpeed", lastMoveDirection.y);
        }

        if (GameManager.instance.gameState == GameState.Dead)
        {
            rigidBody.linearVelocity = Vector2.zero;
            return;
        }

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
        if (GameManager.instance.gameState == GameState.Dead)
        {
            return;
        }
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
        if (GameManager.instance.gameState == GameState.Dead)
        {
            return;
        }
        direction = ctx.ReadValue<Vector2>().normalized;
        if (GameManager.instance.gameState == GameState.Inventory)
        {
            GameManager.instance.SetGameState(GameState.InGame);
        }
    }

    public void HandleDashInput(InputAction.CallbackContext ctx)
    {
        if (GameManager.instance.gameState == GameState.Dead)
        {
            return;
        }
        if (ctx.performed && dashTimer <= 0 && GameManager.instance.gameState == GameState.InGame)
        {
            StartCoroutine(Dash());
        }
    }

}
