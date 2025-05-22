using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    [Header("Components")]
    private PlayerAbilities playerAbilities;

    [Header("InteractionStats")]
    private float pickupRange = 1.5f;

    private void Start()
    {
        playerAbilities = GetComponentInChildren<PlayerAbilities>();
    }

    private bool CanUseAbility(int abilityIndex)
    {
        return playerAbilities.abilities[abilityIndex].GetComponent<Ability>().CheckCooldown();
    }

    // Left Mouse Button
    public void HandleAttackInputOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(0))
        {
            playerAbilities.abilities[0].GetComponent<Ability>().Activate();
        }
    }

    // Right Mouse BUtton
    public void HandleAttackInputTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(1))
        {
            playerAbilities.abilities[1].GetComponent<Ability>().Activate();
        }
    }

    // Shift
    public void HandleAttackInputThree(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(2))
        {
            playerAbilities.abilities[2].GetComponent<Ability>().Activate();
        }
    }

    // R
    public void HandleAttackInputFour(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(3))
        {
            playerAbilities.abilities[3].GetComponent<Ability>().Activate();
        }
    }

    // E
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

    // TAB
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
