using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cut : MonoBehaviour
{
    public Material cutEffectMaterial;
    public float cutDuration = 2f;
    public float returnDuration = 2f;

    private float cutLine = 0.5f;
    private float offset = 0f;

    void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame) // Trigger the effect
        {
            StartCoroutine(PlayCutEffect());
        }
    }

    private IEnumerator PlayCutEffect()
    {
        float elapsedTime = 0f;

        // Animate the cut
        while (elapsedTime < cutDuration)
        {
            elapsedTime += Time.deltaTime;
            offset = Mathf.Lerp(0f, 0.5f, elapsedTime / cutDuration);
            cutEffectMaterial.SetFloat("_Offset", offset);
            yield return null;
        }

        // Pause
        yield return new WaitForSeconds(1f);

        // Animate the return
        elapsedTime = 0f;
        while (elapsedTime < returnDuration)
        {
            elapsedTime += Time.deltaTime;
            offset = Mathf.Lerp(0.5f, 0f, elapsedTime / returnDuration);
            cutEffectMaterial.SetFloat("_Offset", offset);
            yield return null;
        }
    }
}
