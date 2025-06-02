using System.Collections;
using UnityEngine;

public interface IMelee
{
    Vector2 direction { get; set; }
    MeleeStats meleeStats { get; set; }

    public void InitiateMelee()
    {
       // no reduced stats
    }

    public void DefaultMeleeBehaviour(GameObject gameObject)
    {
        MonoBehaviour monoBehaviour = gameObject.GetComponent<MonoBehaviour>();
        monoBehaviour.StartCoroutine(SwingMeleeOnce(gameObject));
    }

    private IEnumerator SwingMeleeOnce(GameObject gameObject)
    {
        float arcAngle = 120f;
        float swingDuration = meleeStats.meleeDuration;

        Vector3 originalScale = gameObject.transform.localScale;
        Vector3 scaledSize = originalScale * meleeStats.meleeSize;
        gameObject.transform.localScale = scaledSize;

        Quaternion initialRotation = gameObject.transform.localRotation;
        Quaternion startRotation = initialRotation * Quaternion.Euler(0, 0, arcAngle / 2);
        Quaternion endRotation = initialRotation * Quaternion.Euler(0, 0, -arcAngle / 2);

        float elapsedTime = 0f;

        while (elapsedTime < swingDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / swingDuration;
            gameObject.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
    }

}
