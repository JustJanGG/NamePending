using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [Header("Damage Popup Prefab")]
    public GameObject damagePopupPrefab;

    public void ShowDamagePopup(int damage, DamageType damageType, Vector2 enemyPosition)
    {
        Vector2 randomOffset = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        Vector2 spawnPosition = enemyPosition + randomOffset + new Vector2(0, 0.2f);

        GameObject damagePopup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);
        damagePopup.GetComponent<TextMeshPro>().SetText(damage.ToString());
        CheckDamageType(damagePopup, damageType);
        //StartCoroutine(HandlePopupEffectsInArc(damagePopup));
        StartCoroutine(HandlePopupEffects(damagePopup));
    }

    private IEnumerator HandlePopupEffectsInArc(GameObject damagePopup)
    {
        // Step 1: Perform the pop-out and arc effect
        yield return StartCoroutine(PopTextInArc(damagePopup, 0.8f, 1f)); // Pop duration: 0.8s, Pop distance: 1 unit

        // Step 2: Wait for 2 seconds before starting the fade-out
        yield return new WaitForSeconds(2f);

        // Step 3: Start fading out the text
        StartCoroutine(FadeOutText(damagePopup.GetComponent<TextMeshPro>(), 1f)); // Fade duration: 1s

        // Destroy the popup after the fade-out is complete
        Destroy(damagePopup, 1.5f); // Ensure it is destroyed after fade-out
    }

    private IEnumerator HandlePopupEffects(GameObject damagePopup)
    {
        // Step 1: Perform the popup effect
        yield return StartCoroutine(PopText(damagePopup, 0.2f, 1.2f));

        // Step 2: Wait for a short delay before starting the other effects
        yield return new WaitForSeconds(0.1f);

        // Step 3: Perform the upward movement and fading effects
        StartCoroutine(MoveTextUpwards(damagePopup, 1f));
        StartCoroutine(FadeOutText(damagePopup.GetComponent<TextMeshPro>(), 1f));

        // Destroy the popup after all effects are complete
        Destroy(damagePopup, 1.1f);
    }

    private IEnumerator MoveTextUpwards(GameObject damagePopup, float duration)
    {
        Vector3 startPosition = damagePopup.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 1f, 0);
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            damagePopup.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            yield return null;
        }
        damagePopup.transform.position = endPosition;
    }

    private IEnumerator FadeOutText(TextMeshPro textMesh, float duration)
    {
        Color originalColor = textMesh.color;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private IEnumerator PopText(GameObject damagePopup, float popDuration, float maxScale)
    {
        Vector3 originalScale = damagePopup.transform.localScale / 2;
        Vector3 targetScale = originalScale * maxScale;
        float elapsedTime = 0f;
        while (elapsedTime < popDuration)
        {
            elapsedTime += Time.deltaTime;
            damagePopup.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / popDuration);
            yield return null;
        }
        damagePopup.transform.localScale = targetScale;
    }

    private IEnumerator PopTextInArc(GameObject damagePopup, float popDuration, float popDistance)
    {
        Vector3 startPosition = damagePopup.transform.position;

        // Randomize the horizontal direction (left or right)
        float randomHorizontalOffset = Random.Range(-0.5f, 0.5f); // Adjust range for desired horizontal spread
        Vector3 peakPosition = startPosition + new Vector3(randomHorizontalOffset, popDistance, 0); // Peak of the arc
        Vector3 endPosition = startPosition + new Vector3(randomHorizontalOffset, 0, 0); // Final position at the same height as start

        float elapsedTime = 0f;

        // Simulate the arc (upward and then downward)
        while (elapsedTime < popDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation factor
            float t = elapsedTime / popDuration;

            // Interpolate horizontally and vertically
            float xPosition = Mathf.Lerp(startPosition.x, endPosition.x, t);
            float yPosition = Mathf.Lerp(startPosition.y, peakPosition.y, t) + (4 * popDistance * t * (1 - t)); // Parabolic arc

            // Update the position of the popup
            damagePopup.transform.position = new Vector3(xPosition, yPosition, startPosition.z);

            yield return null;
        }

        // Ensure the final position is set
        damagePopup.transform.position = endPosition;
    }

    private void CheckDamageType(GameObject damagePopup, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                damagePopup.GetComponent<TextMeshPro>().color = Color.white;
                break;
            case DamageType.Fire:
                damagePopup.GetComponent<TextMeshPro>().color = Color.red;
                break;
            case DamageType.Cold:
                damagePopup.GetComponent<TextMeshPro>().color = Color.blue;
                break;
            case DamageType.Lightning:
                damagePopup.GetComponent<TextMeshPro>().color = Color.yellow;
                break;
        }
    }
}
