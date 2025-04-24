using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [Header("Damage Popup Prefab")]
    public GameObject damagePopupPrefab;

    public void ShowDamagePopup(int damage, DamageType damageType, Vector2 enemyPosition)
    {
        GameObject damagePopup = Instantiate(damagePopupPrefab, new Vector2(enemyPosition.x, enemyPosition.y + 0.2f), Quaternion.identity);
        damagePopup.GetComponent<TextMeshPro>().SetText(damage.ToString());
        CheckDamageType(damagePopup, damageType);
        StartCoroutine(MoveTextUpwards(damagePopup, 1f));
        StartCoroutine(FadeOutText(damagePopup.GetComponent<TextMeshPro>(), 1f));
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
