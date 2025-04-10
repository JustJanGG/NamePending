using System;
using System.Collections;
using Unity.VisualScripting;
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
        Move();
        CheckSortingLayer();
    }

    private void Move()
    {
            movementSpeed = GetComponent<EnemyStats>().movementSpeed;
            aggroRange = GetComponent<EnemyStats>().aggroRange;

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
    }

    private void CheckSortingLayer()
    {
        // TODO: maybe change it so the maxY and minY is not limited!
        // Calculate the sorting order based on the y position of the enemy
        float minY = -10f;
        float maxY = 10f;
        float normalizedY = (transform.position.y - minY) / (maxY - minY);
        int sortingOrder = Mathf.Clamp((int)(normalizedY * 100), 0, 100) *-1;

        GetComponent<Renderer>().sortingOrder = sortingOrder;

        if (player.transform.position.y < this.transform.position.y)
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("EnemyAbovePlayer");
        }
        else
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("EnemyBelowPlayer");
        }
    }
}
