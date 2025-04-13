using UnityEngine;

public class GreenCircuit : ICircuit
{
    public int id { get; set; }
    public string circuitName { get; set; }
    public string circuitDescription { get; set; }
    public CircuitType circuitType { get; set; }

}
