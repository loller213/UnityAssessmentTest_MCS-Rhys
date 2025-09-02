using System.Collections;
using UnityEngine;

public static class SimpleTween
{
    public static IEnumerator MoveTo(Transform target, Vector3 end, float duration)
    {
        Vector3 start = target.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            target.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        target.position = end; // snap to final
    }

    public static IEnumerator ScaleTo(Transform target, Vector3 end, float duration)
    {
        Vector3 start = target.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            target.localScale = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        target.localScale = end;
    }

    public static IEnumerator FadeTo(CanvasGroup canvasGroup, float end, float duration)
    {
        float start = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}
