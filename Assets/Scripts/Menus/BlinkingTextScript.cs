using UnityEngine;
using UnityEngine.SceneManagement;

public class BlinkingTextScript : MonoBehaviour
{
    public BlinkingTextScript blinkingText;
    public bool tick;
    public GameObject canvas;
    string sceneName;

    void BlinkingText()
    {
        if (canvas.activeInHierarchy == true)
        {
            if (tick == true)
            {
                blinkingText.gameObject.SetActive(false);
                tick = false;
            }
            else
            {
                blinkingText.gameObject.SetActive(true);
                tick = true;
            }
        }
    }

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "GameOverScene") 
        {
            tick = true;
            blinkingText = GetComponent<BlinkingTextScript>();
            InvokeRepeating("BlinkingText", 0f, 1f);
        } 
        else
        {
            Invoke("SetTickToTrue", 1);
            blinkingText = GetComponent<BlinkingTextScript>();
            InvokeRepeating("BlinkingText", 0f, 1f);
        }
    }

    public void SetTickToTrue()
    {
        tick = true;
    }
}
   
