using UnityEngine;

public abstract class Circuit : MonoBehaviour
{
    protected int id;
    protected string circuitName;
    protected string circuitDescription;
    public CircuitType circuitType;

}
public enum CircuitType
{
    Green,
    Red,
    Blue
}
