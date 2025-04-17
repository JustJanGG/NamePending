using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;
    private Camera mainCamera;
    private GameObject ui;

    Vector2 draggableWorldPosition;
    Vector3 mousePositionOffset;
    private bool isHolding = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = player.GetComponentInChildren<Camera>();
        ui = GameObject.FindGameObjectWithTag("UI");
    }

    private void Update()
    {
        if (!isHolding)
        {
            CheckSortingLayer();
        }

        if (isHolding)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, mainCamera.ScreenToWorldPoint(Input.mousePosition + mousePositionOffset), 50f * Time.deltaTime);
            
            if (Input.GetMouseButtonDown(0))
            {
                GameObject circuitSlotUi = ui.GetComponent<UIManager>().IsPointerOverUIElement();
                if (circuitSlotUi != null)
                {
                    Debug.Log(circuitSlotUi);
                    SetAbilityToPlayer(circuitSlotUi);
                    this.GetComponent<Renderer>().enabled = false;
                    this.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    DropDraggable();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                DropDraggable();
            }
        }
    }

    public void Pickup()
    {
        mousePositionOffset = gameObject.transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        draggableWorldPosition = gameObject.transform.position;
        isHolding = true;
    }

    private void DropDraggable()
    {
        isHolding = false;
        transform.position = draggableWorldPosition;
    }

    private void SetAbilityToPlayer(GameObject circuitSlotUi)
    {
        Transform playerAbilites = player.GetComponentInChildren<PlayerAbilities>().gameObject.transform;
        Debug.Log(playerAbilites.Find(circuitSlotUi.transform.name));
        // TODO: change index to ability index 
        transform.SetParent(playerAbilites.GetChild(0).Find(circuitSlotUi.transform.name));
    }

    private void CheckSortingLayer()
    {
        // TODO: maybe change it so the maxY and minY is not limited!
        // Calculate the sorting order based on the y position of the enemy
        float minY = -10f;
        float maxY = 10f;
        float normalizedY = (transform.position.y - minY) / (maxY - minY);
        int sortingOrder = Mathf.Clamp((int)(normalizedY * 100), 0, 100) * -1;

        GetComponent<Renderer>().sortingOrder = sortingOrder;

        if (player.transform.position.y < this.transform.position.y)
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("DraggableAbovePlayer");
        }
        else
        {
            GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("DraggableBelowPlayer");
        }
    }

}
