using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using System;
public abstract class Ability : MonoBehaviour
{
    public GameObject abilityPrefab;
    public List<GameObject> circuits;

    protected GameObject player;
    protected int id;
    protected string abilityName;
    protected string abilityDescription;
    protected float cooldown;
    protected List<Tag> tags;
    protected float physicalDamage;
    protected float fireDamage;

    [Header("General Stats")]
    public float procCoefficiant;

    [Header("Projectile Stats")]
    public float projectileSpeed;
    public int projectileCount;

    [Header("Area of Effect Stats")]
    public float areaOfEffect;

    public List<IBlueCircuit> GetBlueCircuits()
    {
        List<IBlueCircuit> blueCircuits = new List<IBlueCircuit>();
        foreach (GameObject circuit in circuits)
        {
            if (circuit.GetComponent<ICircuit>().circuitType == CircuitType.Blue)
            {
                blueCircuits.Add(circuit.GetComponent<IBlueCircuit>());
            }
        }
        return blueCircuits;
    }
    public void ProcBlueCircuit(Hit hit, IBlueCircuit blueCircuit, GameObject enemy)
    {

    }
    public abstract void UseAbility(InputAction.CallbackContext ctx);
    public abstract float[] DealDamage();
}
