using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;
    private Camera mainCamera;

    Vector3 mousePositionOffset;
    private bool isHolding = false;

    //public TMPro.TextMeshPro tooltipText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = player.GetComponentInChildren<Camera>();
        //tooltipText = this.GetComponentInChildren<TMPro.TextMeshPro>();
    }

    private void Update()
    {
        if (!isHolding)
        {
            CheckSortingLayer();
        }

        if (isHolding)
        {
            //player.GetComponent<PlayerController>().enabled = false;
            //this.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition + mousePositionOffset);
            this.transform.position = Vector2.MoveTowards(this.transform.position, mainCamera.ScreenToWorldPoint(Input.mousePosition + mousePositionOffset), 50f * Time.deltaTime);
        }
        //float distance = Vector3.Distance(player.transform.position, transform.position);

        //if (distance < 1.0f)
        //{
        //    //Debug.Log("Show Tooltip");
        //    //tooltipText.gameObject.SetActive(true);
        //    //tooltipText.transform.position = mainCamera.WorldToScreenPoint(transform.position + Vector3.up * 0.5f);
        //}
    }

    //private void OnMouseDrag()
    //{
    //    if (isHolding)
    //    {
    //        this.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition + mousePositionOffset);
    //    }
    //}

    public void Pickup()
    {
        mousePositionOffset = gameObject.transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        isHolding = true;
    }

    //private void OnMouseUp()
    //{
    //    isHolding = false;
    //}

    //private void OnMouseExit()
    //{
    //    if (isHolding)
    //    {
    //        isHolding = false;
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    if (isHolding)
    //    {
    //        isHolding = false;
    //    }
    //}

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
