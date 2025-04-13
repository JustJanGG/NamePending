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
        playerCollider.enabled = false;

        Vector2 dashDirection = direction.normalized;
        if (dashDirection == Vector2.zero)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            dashDirection = (mousePosition - (Vector2)transform.position).normalized;
        }

        rigidBody.AddForce(dashDirection * playerStats.dashRange, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        dashTimer = playerStats.dashCooldown;
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

    // TODO: change with enemyList
    public void HandleCircuitPickup(InputAction.CallbackContext ctx)
    {
        Collider2D circleCollider = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Circuit"));
        if (ctx.performed && circleCollider)
        {
            Debug.Log("Pickup Circuit");
            float minDistance = 0f;
            GameObject circuitToPickup = null;
            foreach (Transform child in circleCollider.transform)
            {
                GameObject circuit = child.gameObject.tag == "Circuit" ? child.gameObject : null;
                Debug.Log("Circuit: " + circuit.name);
                if (circuit == null) continue;

                float distanceToCircuit = Vector2.Distance(transform.position, circuit.transform.position);
                if (minDistance == 0f || distanceToCircuit < minDistance)
                {
                    minDistance = distanceToCircuit;
                    circuitToPickup = circuit;
                }
            }
            // pickup circuitToPickup
            if (circuitToPickup != null)
            {
                Debug.Log("Pickup Circuit: " + circuitToPickup.name);
                circuitToPickup.GetComponent<Draggable>().Pickup();
            }
        }
    }

}
