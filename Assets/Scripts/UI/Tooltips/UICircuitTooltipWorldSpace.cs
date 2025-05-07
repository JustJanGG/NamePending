using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICircuitTooltipWorldSpace : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public LayoutElement layoutElement;

    void Update()
    {
        layoutElement.enabled = headerText.preferredWidth >= layoutElement.preferredWidth;
    }

}
