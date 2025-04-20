using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBarManager : MonoBehaviour
{
    private GameObject player;
    private Transform playerAbilites;
    private GameObject ui;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilites = player.GetComponentInChildren<PlayerAbilities>().gameObject.transform;
        ui = GameObject.FindGameObjectWithTag("UI");
    }

    private void Update()
    {
        RemoveCircuit();
    }

    private void RemoveCircuit()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject circuitSlotUi = ui.GetComponent<UIManager>().GetPointerOverCircuitSlotUIElement();
            if (circuitSlotUi != null)
            {
                GameObject ability = playerAbilites.Find(circuitSlotUi.transform.parent.name).GetChild(0).gameObject;
                if (ability.transform.Find(circuitSlotUi.transform.name).childCount != 0)
                {
                    GameObject circuit = ability.transform.Find(circuitSlotUi.transform.name).GetChild(0).gameObject;
                    ability.GetComponent<Ability>().RemoveCircuit(circuit);
                    circuit.transform.SetParent(null);
                    circuitSlotUi.GetComponent<Image>().color = Color.white;

                    circuit.GetComponent<Draggable>().Pickup();
                    Vector3 randomPoint = Random.insideUnitCircle.normalized * Random.Range(0.1f, 0.3f);
                    circuit.GetComponent<Draggable>().draggableWorldPosition = player.transform.position + randomPoint;
                }
            }
        }
    }

    public void ShowCircuitsInAbilitybar(GameObject circuitSlotUi)
    {
        GameObject ability = playerAbilites.Find(circuitSlotUi.transform.parent.name).GetChild(0).gameObject;
        switch (ability.transform.Find(circuitSlotUi.transform.name).GetComponentInChildren<ICircuit>().circuitType)
        {
            case CircuitType.Blue:
                circuitSlotUi.GetComponent<Image>().color = Color.blue;
                break;

            case CircuitType.Red:
                circuitSlotUi.GetComponent<Image>().color = Color.red;
                break;

            case CircuitType.Green:
                circuitSlotUi.GetComponent<Image>().color = Color.green;
                break;

            default:
                break;
        }
    }
}
