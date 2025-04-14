using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RedCircuit : MonoBehaviour, ICircuit
{
    public int id { get; set; }
    public string circuitName { get; set; }
    public string circuitDescription { get; set; }
    public CircuitType circuitType { get; set; }
    public List<Tag> tags;
    public abstract void ApplyRedCircuit(Ability ability);

}
