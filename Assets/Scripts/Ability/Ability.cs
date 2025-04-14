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
    //protected int id;
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

    public List<BlueCircuit> GetBlueCircuits()
    {
        List<BlueCircuit> blueCircuits = new List<BlueCircuit>();
        foreach (GameObject circuit in circuits)
        {
            if (circuit.GetComponent<ICircuit>().circuitType == CircuitType.Blue)
            {
                blueCircuits.Add(circuit.GetComponent<BlueCircuit>());
            }
        }
        return blueCircuits;
    }
    public void ProcBlueCircuit(BlueCircuit blueCircuit, GameObject enemy, List<BlueCircuit> reducedList, float[] damage)
    {
        blueCircuit.Activate(enemy, reducedList, damage);
    }
    public abstract void Activate();
    public abstract float[] DealDamage();
    public abstract void Hit(GameObject enemy);
}
