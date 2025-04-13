using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EmberPrefab : MonoBehaviour
{
    private Vector2 direction;
    private Ember fireballAbility;
    private List<BlueCircuit> reducedList;

    [Header("Fireball properties")]
    public float speed = 5.0f;

    void Start()
    {
        fireballAbility = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Ember>();
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f);
    }
    public void PassList(List<BlueCircuit> reducedList)
    {
        this.reducedList = reducedList;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            fireballAbility.Hit(collision.gameObject, reducedList);
        }
    }
}
