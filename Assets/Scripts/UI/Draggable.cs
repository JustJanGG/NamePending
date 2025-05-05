using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;
    private Camera mainCamera;
    private GameObject ui;
    private Transform playerAbilites;

    public Vector2 draggableWorldPosition;
    Vector3 mousePositionOffset;

    private bool isHolding = false;
    private bool fromGround = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = player.GetComponentInChildren<Camera>();
        ui = GameObject.FindGameObjectWithTag("UI");
        playerAbilites = player.GetComponentInChildren<PlayerAbilities>().gameObject.transform;
    }

    private void Update()
    {
        if (!isHolding)
        {
            CheckSortingLayer();
            draggableWorldPosition = gameObject.transform.position;
            fromGround = false;
        }

        if (isHolding)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, mainCamera.ScreenToWorldPoint(Input.mousePosition + mousePositionOffset), 50f * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject circuitSlotUi = ui.GetComponent<UIManager>().GetPointerOverCircuitSlotUIElement();
                if (circuitSlotUi != null)
                {
                    GameObject ability = playerAbilites.Find(circuitSlotUi.transform.parent.name).GetChild(0).gameObject;

                    if (ability.transform.Find(circuitSlotUi.transform.name).childCount == 0)
                    {
                        isHolding = false;
                        this.GetComponent<Renderer>().enabled = false;
                        this.GetComponent<Collider2D>().enabled = false;
                        SetAbilityToPlayer(circuitSlotUi);
                        if (fromGround)
                        {
                            GameManager.instance.SetGameState(GameState.InGame);
                        }
                    }
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

        if (isHolding && GameManager.instance.gameState == GameState.InGame)
        {
            DropDraggable();
        }
    }

    public void Pickup(bool fromGround)
    {
        Renderer renderer = this.GetComponent<Renderer>();
        renderer.enabled = true;
        this.gameObject.layer = LayerMask.NameToLayer("UI");
        renderer.sortingLayerID = SortingLayer.NameToID("UI");
        renderer.sortingOrder = 100;
        mousePositionOffset = gameObject.transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        isHolding = true;
        this.fromGround = fromGround;
        GameManager.instance.circuitList.Remove(this.gameObject);
    }

    private void DropDraggable()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Circuit");
        Renderer renderer = this.GetComponent<Renderer>();
        renderer.sortingLayerID = SortingLayer.NameToID("DraggableBelowPlayer");
        renderer.sortingOrder = 0;
        isHolding = false;
        transform.position = draggableWorldPosition;
        this.GetComponent<Collider2D>().enabled = true;
        GameManager.instance.circuitList.Add(this.gameObject);
        if (fromGround)
        {
            GameManager.instance.SetGameState(GameState.InGame);
        }
    }

    private void SetAbilityToPlayer(GameObject circuitSlotUi)
    {
        playerAbilites = player.GetComponentInChildren<PlayerAbilities>().gameObject.transform;
        GameObject ability = playerAbilites.Find(circuitSlotUi.transform.parent.name).GetChild(0).gameObject;

        transform.SetParent(ability.transform.Find(circuitSlotUi.transform.name));
        ability.GetComponent<Ability>().ApplyCircuit(this.gameObject);

        ui.GetComponentInChildren<AbilityBarManager>().ShowCircuitsInAbilitybar(circuitSlotUi);
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
