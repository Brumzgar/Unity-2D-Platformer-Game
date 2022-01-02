using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float cameraShakeMagnitude = 1;

    public void Update()
    {
        if (Time.timeScale == 1f)
        {
            StartCoroutine(Shake(cameraShakeMagnitude));
        }
    }

    public IEnumerator Shake(float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float x = Random.Range(-0.5f, 0.5f) * magnitude;
        float y = Random.Range(-0.5f, 0.5f) * magnitude;

        transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

        yield return null;
    }
}
