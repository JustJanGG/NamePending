using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EmberPrefab : BlueCircuitProjectile
{
    public Transform target;

    void Start()
    {
        direction = target.position - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f);
    }
}
