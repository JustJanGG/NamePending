using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICircuitTooltipInventory : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI contentText;

    [Header("Components")]
    private Transform playerAbilites;
    private GameObject ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI");
        playerAbilites = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAbilities>().gameObject.transform;
    }


    void Update()
    {
        if (GameManager.instance.gameState == GameState.Inventory)
        {
            GameObject circuitSlotUi = ui.GetComponent<UIManager>().GetPointerOverCircuitSlotUIElement();
            if (circuitSlotUi != null)
            {
                GameObject ability = playerAbilites.Find(circuitSlotUi.transform.parent.name).GetChild(0).gameObject;
                if (ability.transform.Find(circuitSlotUi.transform.name).childCount != 0)
                {
                    this.headerText.text = ability.transform.Find(circuitSlotUi.transform.name).GetChild(0).GetComponentInChildren<CircuitTooltip>().circuitName;
                    this.gameObject.transform.Find("CircuitTooltipInventory").gameObject.SetActive(true);
                }
            }
            else
            {
                this.gameObject.transform.Find("CircuitTooltipInventory").gameObject.SetActive(false);
            }
        }
        else
        {
            this.gameObject.transform.Find("CircuitTooltipInventory").gameObject.SetActive(false);
        }
    }

}
