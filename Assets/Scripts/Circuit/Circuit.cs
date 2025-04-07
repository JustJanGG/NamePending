using UnityEngine;

public abstract class Circuit
{
    protected int id;
    protected string circuitName;
    protected string circuitDescription;
    protected CircuitType circuitType;

}
public enum CircuitType
{
    Green,
    Red,
    Blue
}
