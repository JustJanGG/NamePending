using UnityEngine;

public class CircuitTooltip : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;
    public GameObject tooltipObject;

    public float tooltipRadius;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= tooltipRadius)
        {
            // Show the tooltip when within range
            if (!tooltipObject.activeSelf)
            {
                tooltipObject.SetActive(true);
            }
            tooltipObject.transform.position = this.transform.position;
            Debug.Log($"Tooltip position: {tooltipObject.transform.position}");
        }
        else
        {
            // Hide the tooltip when out of range
            if (tooltipObject.activeSelf)
            {
                tooltipObject.SetActive(false);
            }
        }
    }
}
