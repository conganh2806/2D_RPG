using System.Collections;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPosition = transform.position;

        var percentComplete = 0.0f;

        while(percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime / duration;


            var currentHeight = Mathf.Sin(percentComplete * Mathf.PI);
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) + Vector3.up * currentHeight;

            percentComplete += Time.deltaTime * duration;
            yield return null;

        }

        gameObject.SetActive(false);
    }
}
