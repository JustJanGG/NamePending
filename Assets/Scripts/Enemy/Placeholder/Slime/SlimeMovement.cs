using System.Collections;
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
    //private bool isEvading;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //isEvading = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //if (!isEvading)
        //{
            movementSpeed = GetComponent<SlimeStats>().movementSpeed;
            aggroRange = GetComponent<SlimeStats>().aggroRange;

            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distanceToPlayer < aggroRange && distanceToPlayer > 1.5f)
            {
                rigidBody.linearVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
            }
            else
            {
                rigidBody.linearVelocity = new Vector2(0, 0);
                direction = Vector2.zero;
            }
        //}
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Enemy"))
    //    {
    //        StartCoroutine(EvadeOtherEnemy());
    //    }
    //}

    //private IEnumerator EvadeOtherEnemy()
    //{
    //    isEvading = true;
    //    rigidBody.linearVelocity = new Vector2(2, 2);
    //    yield return new WaitForSeconds(0.5f);
    //    isEvading = false;
    //}
}
