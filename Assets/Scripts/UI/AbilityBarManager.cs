using UnityEngine;

public class AbilityBarManager : MonoBehaviour
{
    private GameObject player;
    private Transform playerAbilites;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilites = player.GetComponentInChildren<PlayerAbilities>().gameObject.transform;
    }

    void Update()
    {
        // TODO: show UI circuit slots with circuit of the slotnumber from playerabilities
    }
}
