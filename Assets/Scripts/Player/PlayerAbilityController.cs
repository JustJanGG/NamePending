using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    [Header("Components")]
    private PlayerAbilities playerAbilities;

    [Header("InteractionStats")]
    private float pickupRange = 1.5f;

    // Cooldown tracking
    private Dictionary<int, float> abilityCooldowns = new Dictionary<int, float>();

    private void Start()
    {
        playerAbilities = GetComponentInChildren<PlayerAbilities>();

        // set cooldowns to 0
        for (int i = 0; i < playerAbilities.abilities.Count; i++)
        {
            abilityCooldowns[i] = 0f;
        }
    }

    private void Update()
    {
        // Update cooldowns
        foreach (var key in new List<int>(abilityCooldowns.Keys))
        {
            if (abilityCooldowns[key] > 0)
            {
                abilityCooldowns[key] -= Time.deltaTime;
            }
        }
    }

    private bool CanUseAbility(int abilityIndex)
    {
        return abilityCooldowns.ContainsKey(abilityIndex) && abilityCooldowns[abilityIndex] <= 0f;
    }

    private void SetAbilityCooldown(int abilityIndex, float cooldown)
    {
        if (abilityCooldowns.ContainsKey(abilityIndex))
        {
            abilityCooldowns[abilityIndex] = cooldown;
        }
    }

    // Left Mouse Button
    public void HandleAttackInputOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(0))
        {
            playerAbilities.abilities[0].GetComponent<Ability>().Activate();
            SetAbilityCooldown(0, playerAbilities.abilities[0].GetComponent<Ability>().cooldown);

        }
    }

    // Right Mouse BUtton
    public void HandleAttackInputTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(1))
        {
            playerAbilities.abilities[1].GetComponent<Ability>().Activate();
            SetAbilityCooldown(1, playerAbilities.abilities[1].GetComponent<Ability>().cooldown);

        }
    }

    // Shift
    public void HandleAttackInputThree(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(2))
        {
            playerAbilities.abilities[2].GetComponent<Ability>().Activate();
            SetAbilityCooldown(2, playerAbilities.abilities[2].GetComponent<Ability>().cooldown);

        }
    }

    // R
    public void HandleAttackInputFour(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.instance.gameState == GameState.InGame && CanUseAbility(3))
        {
            playerAbilities.abilities[3].GetComponent<Ability>().Activate();
            SetAbilityCooldown(3, playerAbilities.abilities[3].GetComponent<Ability>().cooldown);

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
