using UnityEngine;

public class SlashPrefab : AbilityPrefab, IMelee
{
    public Vector2 direction { get; set; }
    public MeleeStats meleeStats { get; set; }

    void Start()
    {
        ((IMelee)this).InitiateMelee();

        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        ((IMelee)this).DefaultMeleeBehaviour(gameObject);
        Destroy(gameObject, meleeStats.meleeDuration + 0.01f);
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DefaultOnTriggerEnter2D(collision);
    }
}
