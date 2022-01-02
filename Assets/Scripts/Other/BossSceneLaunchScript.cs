using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneLaunchScript : MonoBehaviour
{
    public Animator endingTransition;
    public GameObject endingTransitionObject;

    public void OnEnable()
    {
        Invoke("WaitBeforeTurnOnObject", 1.5f);
        Invoke("WaitBeforeLoadLevel", 2.5f);
    }

    public void WaitBeforeLoadLevel()
    {
        SceneManager.LoadScene("BossScene");
    }

    public void WaitBeforeTurnOnObject() 
    {
        endingTransitionObject.SetActive(true);
        endingTransition.SetTrigger("Start");
    }
}
