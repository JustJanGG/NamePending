using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    int uiLayer;
    GameObject player;

    private void Start()
    {
        uiLayer = LayerMask.NameToLayer("UI");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Returns the gameobject of the Ui element hovered if touched.
    public GameObject GetPointerOverCircuitSlotUIElement()
    {
        return IsPointerOverCircuitSlotUIElement(GetEventSystemRaycastResults());
    }

    private GameObject IsPointerOverCircuitSlotUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == uiLayer && curRaysastResult.gameObject.name.StartsWith("CircuitSlot"))
                return curRaysastResult.gameObject;
        }
        return null;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
