using UnityEngine;

public interface IBlueCircuit
{
    float procCoefficient { get; set; }
    float procChance { get; set; }

    public bool Proc(float procCoefficient)
    {
        if (Random.Range(1, 101) <= procChance * 100)
        {
            return true;
        }
        return false;
    }
}
