using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoltenRockPrefab : AbilityPrefab, IProjectile, IAoE, IBlueCircuitPrefab
{
    [HideInInspector]
    public Transform enemy;
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    public ProjecileStats projecileStats { get; set; }
    public Vector2 direction { get; set; }
    public int pierceCount { get; set; }
    public AoEStats aoeStats { get; set; }

    private void Start()
    {
        ((IProjectile)this).InitiateProjectile();
        StartCoroutine(MoltenRockBehaviour());
        Destroy(gameObject, prefabOf.GetComponent<Ability>().lifetime);
    }

    private IEnumerator MoltenRockBehaviour()
    {
        // circle values
        float radius = 3f;
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        Vector3 start = transform.position;
        Vector3 target = start + new Vector3(randomCircle.x, randomCircle.y, 0f);

        // parabolic arc values
        float duration = 1f / projecileStats.projectileSpeed;
        float elapsed = 0f;
        float arcHeight = radius * 0.5f;
        Vector3 prevPos = start;

        // Shadow setup
        Transform shadow = transform.Find("Shadow");
        if (shadow != null)
            shadow.gameObject.SetActive(true);

        // move in an arc towards the target
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Vector3 pos = Vector3.Lerp(start, target, t);
            pos.y += arcHeight * 4 * t * (1 - t);

            Vector3 velocity = pos - prevPos;
            if (velocity.sqrMagnitude > 0.000001f)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            // shadow
            if (shadow != null)
            {
                Vector3 shadowPos = pos;
                shadowPos.y = Mathf.Min(start.y, target.y); // ground level (could also use start.y if always flat)
                shadow.position = shadowPos;

                // Optional: scale shadow based on height above ground
                float height = pos.y - shadowPos.y;
                float minScale = 0.5f, maxScale = 1.0f;
                float scale = Mathf.Lerp(maxScale, minScale, height / arcHeight);
                shadow.localScale = new Vector3(scale, scale, 1f);
            }

            transform.position = pos;
            prevPos = pos;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // final position, direction and rotation
        transform.position = target;
        Vector3 finalDir = target - prevPos;
        if (finalDir.sqrMagnitude > 0.000001f)
        {
            float angle = Mathf.Atan2(finalDir.y, finalDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Hide shadow after landing
        if (shadow != null)
            shadow.gameObject.SetActive(false);

        // change to AoE behaviour
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        transform.Find("ExplosionSprite").gameObject.SetActive(true);
        ((IAoE)this).DefaultAoEBehaviour(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ((IBlueCircuitPrefab)this).DefaultBlueCircuitOnTriggerEnter2D(collision, prefabOf);
    }
}
