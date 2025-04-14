using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    [Header("Components")]
    private PlayerStats playerStats;

    [Header("InteractionStats")]
    private float pickupRange = 1.5f;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void HandleAbilityInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            playerStats.abilites[0].GetComponent<Ability>().Activate();
        }
    }

    public void HandleInteractAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameObject circuitToPickup = null;
            float minDistance = pickupRange + 0.1f;
            foreach (GameObject circuit in GameManager.instance.circuitList)
            {
                float distanceToCircuit = Vector2.Distance(transform.position, circuit.transform.position);
                if (distanceToCircuit < pickupRange && distanceToCircuit < minDistance)
                {
                    circuitToPickup = circuit;
                    minDistance = distanceToCircuit;
                }
            }
            if (circuitToPickup != null)
            {
                Debug.Log("Pickup Circuit: " + circuitToPickup.name);
                circuitToPickup.GetComponent<Draggable>().Pickup();
            }
        }
    }

}
