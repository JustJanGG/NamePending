using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    [Header("Components")]
    private PlayerStats playerStats;

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

}
