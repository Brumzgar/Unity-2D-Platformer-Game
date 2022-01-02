using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadingTextScript : MonoBehaviour
{
    public bool flag;

    private void OnEnable()
    {
        flag = true;
        if (flag == true)
        {
            StartCoroutine(FadeTextToFullAlpha(3f, GetComponent<Text>()));
            flag = false;
        }
    }

    private void OnDisable()
    {
        flag = false;
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while(i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
