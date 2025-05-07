using TMPro;
using UnityEngine;

public class CircuitTooltip : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;
    public GameObject tooltipObject;
    private TextMeshProUGUI headerText;

    [Header("Tooltip Values")]
    public float tooltipRadius;
    public string circuitName;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        headerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= tooltipRadius)
        {
            if (!tooltipObject.activeSelf)
            {
                tooltipObject.SetActive(true);
                headerText.text = circuitName;
            }
            tooltipObject.transform.position = this.transform.position;
        }
        else
        {
            if (tooltipObject.activeSelf)
            {
                tooltipObject.SetActive(false);
            }
        }
    }
}
