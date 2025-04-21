using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    [Header("Components")]
    private PlayerAbilities playerAbilities;

    [Header("InteractionStats")]
    private float pickupRange = 1.5f;
    
    private void Awake()
    {
        playerAbilities = GetComponentInChildren<PlayerAbilities>();
    }

    public void HandleAbilityInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame)
        {
            playerAbilities.abilities[0].GetComponent<Ability>().Activate();
        }
    }

    public void HandleInteractAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame)
        {
            GameObject circuitToPickup = null;
            float minDistance = pickupRange + 0.1f;
            foreach (GameObject circuit in GameManager.instance.circuitList)
            {
                float distanceToCircuit = Vector2.Distance(transform.position, circuit.transform.position);
                if (distanceToCircuit < pickupRange && distanceToCircuit < minDistance && circuit.GetComponent<Collider2D>().enabled)
                {
                    circuitToPickup = circuit;
                    minDistance = distanceToCircuit;
                }
            }
            if (circuitToPickup != null)
            {
                circuitToPickup.GetComponent<Draggable>().Pickup(true);
                GameManager.instance.SetGameState(GameState.Inventory);
            }
        }
    }

    public void HandleInventoryScreen(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame)
        {
            GameManager.instance.SetGameState(GameState.Inventory);
        }
        else if (ctx.performed && GameManager.instance.gameState == GameState.Inventory)
        {
            GameManager.instance.SetGameState(GameState.InGame);
        }
    }

}
