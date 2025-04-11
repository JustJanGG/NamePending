using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
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

    // TODO: change this position after EnemyList is implemented
    private static float minPositionY;
    private static float maxPositionY;

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
        // TODO: change -50, 50 to minPositionY, maxPositionY after EnemyList is implemented
        GetComponent<Renderer>().sortingOrder = MapSortingOrder(transform.position.y, -50, 50, -100, 100) *-1;

        if (player.transform.position.y < this.transform.position.y)
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("EnemyAbovePlayer");
        }
        else
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("EnemyBelowPlayer");
        }
    }

    private int MapSortingOrder(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (int)((x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min);
    }
}
