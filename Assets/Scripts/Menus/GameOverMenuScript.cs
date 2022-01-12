using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public static bool gameIsOver;
    public Animator gameOverAnimations;
    public GameObject objectToTurnOff;
    string sceneName;
    public bool inputs;

    void Start()
    {
        gameIsOver = true;
        sceneName = SceneManager.GetActiveScene().name;
        FindObjectOfType<AudioManager>().Stop("BossSceneMusic");
        FindObjectOfType<AudioManager>().Stop("GamePausedMusic");
    }

    public void OnEnable()
    {
        inputs = false;
        gameIsOver = true;
        FindObjectOfType<AudioManager>().Play("GameOverPlayerDeath");
        Invoke("ObjectToTurnOffAfterTime", 1.5f);
        Invoke("PlayPlayerExplosionSoundAfterTime", 1f);
        Invoke("GameOverCanvasSoundAfterTime", 1.75f);
        Invoke("TurnOnInputsAfterTime", 4.5f);

    }

    public void Update()
    {
        if (inputs == true)
        {
            if (Input.GetButtonDown("Submit"))
                FindObjectOfType<AudioManager>().Play("MenuConfirm");           

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W)))
                FindObjectOfType<AudioManager>().Play("MenuMove");
        }
    }

    public void OnDisable()
    {
        gameIsOver = false;
        objectToTurnOff.SetActive(true);
    }

    public void RestartLevel()
    {
        inputs = false;
        FindObjectOfType<AudioManager>().Play("MenuConfirm");
        gameOverAnimations.SetTrigger("GameOverScreenFade");
        Invoke("RestartGameAfterTime", 1);
    }

    public void QuitToMenu()
    {
        inputs = false;
        FindObjectOfType<AudioManager>().Play("MenuConfirm");
        gameOverAnimations.SetTrigger("GameOverScreenFade");
        Invoke("QuitToMenuAfterTime", 1);
    }

    public void RestartGameAfterTime()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitToMenuAfterTime()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ObjectToTurnOffAfterTime()
    {
        objectToTurnOff.SetActive(false);
    }

    public void PlayPlayerExplosionSoundAfterTime()
    {
        FindObjectOfType<AudioManager>().Play("GameOverPlayerExplosion");
    }

    public void GameOverCanvasSoundAfterTime()
    {
        FindObjectOfType<AudioManager>().Play("GameOverCanvas");
    }

    public void TurnOnInputsAfterTime()
    {
        inputs = true;
    }
}
