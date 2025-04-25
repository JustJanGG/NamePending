using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    int uiLayer;
    private void Awake()
    {
       this.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        uiLayer = LayerMask.NameToLayer("UI");
        transform.Find("TransparentBackground").GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        transform.Find("TransparentBackground").GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
    }

    private void Update()
    {
        if (GameManager.instance.gameState == GameState.Inventory)
        {
            GameObject abilityBar = GetComponentInChildren<AbilityBarManager>().gameObject;
            abilityBar.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            abilityBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(abilityBar.GetComponent<RectTransform>().anchoredPosition.x, 150);
            transform.Find("TransparentBackground").gameObject.SetActive(true);
        }
        else
        {
            ResetAbilityBar();
        }
    }

    private void ResetAbilityBar()
    {
        GameObject abilityBar = GetComponentInChildren<AbilityBarManager>().gameObject;
        abilityBar.transform.localScale = new Vector3(1f, 1f, 1f);
        abilityBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(abilityBar.GetComponent<RectTransform>().anchoredPosition.x, 50);
        transform.Find("TransparentBackground").gameObject.SetActive(false);
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
