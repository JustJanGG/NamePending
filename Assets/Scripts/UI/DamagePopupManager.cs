using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [Header("Damage Popup Prefab")]
    public GameObject damagePopupPrefab;

    public void ShowDamagePopup(int damage, DamageType damageType, Vector2 enemyPosition)
    {
        Vector2 randomOffset = new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        Vector2 spawnPosition = enemyPosition + randomOffset + new Vector2(0, 0.2f);

        GameObject damagePopup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);
        damagePopup.GetComponent<TextMeshPro>().SetText(damage.ToString());
        CheckDamageType(damagePopup, damageType);
        StartCoroutine(HandlePopupEffects(damagePopup, damage));
    }

    private IEnumerator HandlePopupEffects(GameObject damagePopup, int damage)
    {
        yield return StartCoroutine(PopText(damagePopup, 0.3f, 1.3f, damage));

        yield return new WaitForSeconds(0.05f);

        StartCoroutine(MoveTextUpwards(damagePopup, 1f));
        StartCoroutine(FadeOutText(damagePopup.GetComponent<TextMeshPro>(), 0.6f));

        Destroy(damagePopup, 1.1f);
    }

    private IEnumerator PopText(GameObject damagePopup, float popDuration, float maxScale, int damage)
    {
        Vector3 originalScale = damagePopup.transform.localScale;
        Vector3 targetScale = originalScale * maxScale;
        Quaternion originalRotation = damagePopup.transform.rotation;

        TextMeshPro textMesh = damagePopup.GetComponent<TextMeshPro>();
        int currentDamage = 0;

        float elapsedTime = 0f;
        while (elapsedTime < popDuration)
        {
            elapsedTime += Time.deltaTime;
            damagePopup.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / popDuration);

            float rotationAngle = Mathf.Sin(elapsedTime * 25f) * 7f;
            damagePopup.transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotationAngle);

            currentDamage = Mathf.RoundToInt(Mathf.Lerp(damage / 2, damage, elapsedTime / popDuration));
            textMesh.SetText(currentDamage.ToString());

            yield return null;
        }
        damagePopup.transform.localScale = targetScale;
        damagePopup.transform.rotation = originalRotation;
        textMesh.SetText(damage.ToString());
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
                damagePopup.GetComponent<TextMeshPro>().color = Color.cyan;
                break;
            case DamageType.Lightning:
                damagePopup.GetComponent<TextMeshPro>().color = Color.yellow;
                break;
        }
    }
}
