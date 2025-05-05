using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI contentText;
    public LayoutElement layoutElement;


    void Update()
    {
        layoutElement.enabled = Mathf.Max(headerText.preferredWidth, contentText.preferredWidth) >= layoutElement.preferredWidth;
    }
}
