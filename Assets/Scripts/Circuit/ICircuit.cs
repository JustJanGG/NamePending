using UnityEngine;

public interface ICircuit
{
    int id {  get; set; }
    string circuitName { get; set; }
    string circuitDescription { get; set; }
    CircuitType circuitType { get; set; }

}
public enum CircuitType
{
    Green,
    Red,
    Blue
}
