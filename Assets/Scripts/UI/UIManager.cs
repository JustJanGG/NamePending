using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private int uiLayer;
    private GameObject abilityBar;
    private float oldXPosition;


    private void Awake()
    {
       this.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
       abilityBar = GetComponentInChildren<AbilityBarManager>().gameObject;
    }

    private void Start()
    {
        uiLayer = LayerMask.NameToLayer("UI");
        transform.Find("TransparentBackground").GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        transform.Find("TransparentBackground").GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        oldXPosition = abilityBar.GetComponent<RectTransform>().anchoredPosition.x;

    }

    private void Update()
    {
        float oldXPosition = abilityBar.GetComponent<RectTransform>().anchoredPosition.x;
        if (GameManager.instance.gameState == GameState.Inventory)
        {
            abilityBar.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            abilityBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(300f, 150f);
            transform.Find("TransparentBackground").gameObject.SetActive(true);
        }
        else
        {
            ResetAbilityBar();
        }
    }

    private void ResetAbilityBar()
    {
        abilityBar.transform.localScale = new Vector3(1f, 1f, 1f);
        abilityBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(oldXPosition, 50f);
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
