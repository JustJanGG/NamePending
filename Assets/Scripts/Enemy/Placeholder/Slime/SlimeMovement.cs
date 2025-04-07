using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [Header("Stats")]
    private float movementSpeed;
    private float aggroRange;

    [Header("Logic")]
    private Rigidbody2D rigidBody;
    public GameObject player;
    private float distanceToPlayer;
    public Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed = GetComponent<SlimeStats>().movementSpeed;
        aggroRange = GetComponent<SlimeStats>().aggroRange;

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distanceToPlayer < aggroRange)
        {
            rigidBody.linearVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
        }
        else
        {
            rigidBody.linearVelocity = new Vector2(0, 0);
            direction = Vector2.zero;
        }
    }
}
