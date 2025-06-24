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
    private GameObject player;
    private float distanceToPlayer;
    public Vector2 direction;

    [Header("Attack")]
    private bool isAttacking;
    public float attackCooldown;
    private float attackCountdown;
    private GameObject hitBox;

    [Header("Animation")]
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAttacking = false;
        attackCountdown = 0f;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        hitBox = transform.Find("AttackHitBox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameState.Dead)
        {
            rigidBody.linearVelocity = Vector2.zero;
            return;
        }
        if (!isAttacking)
        {
            Move();
            Animate();
        }
        if (distanceToPlayer < aggroRange && distanceToPlayer < 1.5f && !isAttacking && attackCountdown <= 0f)
        {
            StartCoroutine(AttackPlayer());
        }
        if (attackCountdown > 0f)
        {
            attackCountdown -= Time.deltaTime;
        }
        CheckSortingLayer();
    }

    private void Move()
    {
        movementSpeed = GetComponent<EnemyStats>().movementSpeed;
        aggroRange = GetComponent<EnemyStats>().aggroRange;

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distanceToPlayer < aggroRange && distanceToPlayer > 1.5f && !isAttacking)
        {
            rigidBody.linearVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
        }
        else
        {
            rigidBody.linearVelocity = Vector2.zero;
            direction = Vector2.zero;
        }
    }

    private void Animate()
    {
        animator.SetFloat("XSpeed", direction.x);
        animator.SetFloat("YSpeed", direction.y);
    }

    private IEnumerator AttackPlayer()
    {
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        isAttacking = true;
        rigidBody.linearVelocity = Vector2.zero;

        Vector2 attackDir = player.transform.position - transform.position;
        direction = attackDir;

        // Set blend tree parameters for attack direction
        animator.SetFloat("XSpeed", direction.x);
        animator.SetFloat("YSpeed", direction.y);

        animator.SetTrigger("startAttack");
        yield return new WaitForSeconds(1f);
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > 3f)
        {
            animator.SetTrigger("cancelAttack");
            attackCountdown = attackCooldown;
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            isAttacking = false;
            yield break;
        }

        animator.SetTrigger("attacking");

        // Lunge parameters
        float lungeDistance = 1f;
        float lungeDuration = 0.05f; // Forward lunge duration
        float returnDuration = 0.1f; // Backward return duration

        Vector2 startPos = GetComponent<SpriteRenderer>().transform.position;
        Vector2 endPos = (Vector2)transform.position + (direction * lungeDistance);

        // Forward lunge
        float elapsed = 0f;
        while (elapsed < lungeDuration)
        {
            hitBox.SetActive(true);
            hitBox.transform.position = endPos;
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / lungeDuration);
            GetComponent<SpriteRenderer>().transform.position = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        // Backward return
        elapsed = 0f;
        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / returnDuration);
            GetComponent<SpriteRenderer>().transform.position = Vector2.Lerp(endPos, startPos, t);
            yield return null;
        }

        hitBox.transform.position = startPos;
        transform.Find("AttackHitBox").gameObject.SetActive(false);
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        attackCountdown = attackCooldown;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerStats>().TakeDamage(GetComponent<EnemyStats>().DealDamage());
        }
    }

    private void CheckSortingLayer()
    {
        // TODO: change -50, 50 to minPositionY, maxPositionY after EnemyList is implemented

        if (player.transform.position.y < this.transform.position.y)
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("EnemyAbovePlayer");
        }
        else
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("EnemyBelowPlayer");
        }
        GetComponent<Renderer>().sortingOrder = MapSortingOrder(transform.position.y, -50, 50, -100, 100) * -1;
    }

    private int MapSortingOrder(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (int)((x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min);
    }
}
