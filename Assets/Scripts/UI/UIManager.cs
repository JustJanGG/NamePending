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

    public void ReceiveDraggable(GameObject draggable)
    {
        // TODO: index 0 is ability slot, need to change to be dynamic
        Transform playerAbilites = player.GetComponentInChildren<PlayerAbilities>().gameObject.transform;
        draggable.transform.SetParent(playerAbilites.GetChild(0));
    }

    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == uiLayer)
                return true;
        }
        return false;
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
