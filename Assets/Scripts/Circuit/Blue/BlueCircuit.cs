using UnityEngine;

public class BlueCircuit : Circuit
{
    protected float procCoefficient;
    protected float procChance;

    public bool Proc(float procCoefficient)
    {
        if (Random.Range(1, 101) <= procChance * 100)
        {
            return true;
        }
        return false;
    }
}
