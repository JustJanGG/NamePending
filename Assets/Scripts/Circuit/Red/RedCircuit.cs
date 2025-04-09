using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RedCircuit : Circuit
{
    public List<Tag> tags;
    public abstract void ApplyRedCircuit(Ability ability);
}
