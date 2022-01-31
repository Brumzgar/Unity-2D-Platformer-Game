using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator transition;
    public static bool gameIsStarting = false;

    public void Start()
    {
        gameIsStarting = false;
    }

    public void OnEnable()
    {
        Debug.Log("----------------MainMenuScript OnEnable--------------------");
        Debug.Log(PlayerPrefs.GetString("showTutorials") + " = showTutorials");
    }

    public void OnDisable()
    {
        gameIsStarting = false;
    }

    public void PlayGame ()
    {
        if (gameIsStarting == false)
        {
            gameIsStarting = true;
            // Sound
            FindObjectOfType<AudioManager>().Play("MenuConfirm");
            Invoke("PlayTransitionStartAfterTime", 0.25f);
            transition.SetTrigger("Start");
            Invoke("LoadGameAfterTime", 1);
        }
    }

    public void QuitGame ()
    {
        if (gameIsStarting == false)
        {
            FindObjectOfType<AudioManager>().Play("PauseMenuDisabled");
            Invoke("QuitGameAfterTime", 0.25f);
        }
    }

    public void LoadGameAfterTime()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PlayTransitionStartAfterTime()
    {
        FindObjectOfType<AudioManager>().Play("TransitionStart");
    }

    public void QuitGameAfterTime()
    {
        Application.Quit();
    }
}
