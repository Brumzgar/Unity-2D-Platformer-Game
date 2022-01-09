using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseOptionsUI;
    public static bool gameIsPausedAndPlayerIsCrouchingCheck = false;
    public static bool gameIsEnding = false;
    public static bool gameIsReady = false;
    public Animator transition;
    public GameObject gameOverScreen;
    public PlayerScript playerScript;
    bool pauseOptionsUIFlag;
    bool pauseFlag;

    public void Start()
    {
        gameIsEnding = false;
        gameIsPaused = false;
        Invoke("SetGameIsReadyToTrue", 1);
    }
    public void QuitToMenu()
    {
        if (gameIsEnding == false)
        {
            gameIsEnding = true;
            pauseMenuUI.SetActive(false);
            transition.SetTrigger("End");
            Invoke("LoadMenuAfterTime", 1);
        }
    }

    public void OnDisable()
    {
        gameIsEnding = false;
        gameIsReady = false;
    }

    public void Update()
    {
        if (pauseOptionsUI.activeInHierarchy && pauseOptionsUIFlag == false)
        {
            FindObjectOfType<AudioManager>().Play("MenuConfirm");
            pauseOptionsUIFlag = true;
        } 
        else if (!pauseOptionsUI.activeInHierarchy)
            pauseOptionsUIFlag = false;

        if (gameIsReady)
        {
            PauseMenuController();
        }

        if (gameIsEnding)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void PauseMenuController()
    {
        if (Input.GetButtonDown("Cancel") && gameIsPaused == false && gameIsEnding == false && GameOverMenuScript.gameIsOver == false)
        {
            if (pauseFlag == false) 
            {
                pauseFlag = true;
                pauseMenuUI.SetActive(true);
            }
        }
        else if (Input.GetButtonDown("Cancel") && gameIsPaused == true && gameIsEnding == false && GameOverMenuScript.gameIsOver == false)
        {
            if (pauseOptionsUI.activeInHierarchy == false) 
            {
                pauseMenuUI.SetActive(false);
            }
        }

        if (pauseMenuUI.activeInHierarchy == true)
        {
            if (!gameIsPaused)
            {
                gameIsPaused = true;
                FindObjectOfType<AudioManager>().Play("PauseMenuEnabled");
            }

            if (GameOverMenuScript.gameIsOver)
                pauseMenuUI.SetActive(false);

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W)))
                FindObjectOfType<AudioManager>().Play("MenuMove");

            FindObjectOfType<AudioManager>().Stop("PlayerRun");
            gameIsPausedAndPlayerIsCrouchingCheck = true;
            Invoke("SetTimeScaleTo0AfterTime", 0.15f); // In this time window all eagle sounds are turned off before game is paused
        }
        else
        {
            Time.timeScale = 1f;

            if (pauseFlag == true)
            {
                Invoke("SetPauseFlagToFalse", 0.25f);
            }

            if (gameIsPaused)
            {
                gameIsPaused = false;
                FindObjectOfType<AudioManager>().Play("PauseMenuDisabled");
            }

            if (GameOverMenuScript.gameIsOver == false)
            {
                FindObjectOfType<AudioManager>().Stop("MenuConfirm");
                FindObjectOfType<AudioManager>().Stop("MenuMove");
            }

            if (PlayerScript.crouch == true && gameIsPausedAndPlayerIsCrouchingCheck == true)
            {
                PlayerScript.crouch = false;
            }
            gameIsPausedAndPlayerIsCrouchingCheck = false;
        }
    }

    public void LoadMenuAfterTime()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void SetGameIsReadyToTrue()
    {
        gameIsReady = true;
    }

    public void SetTimeScaleTo0AfterTime()
    {
        Time.timeScale = 0f;
    }

    public void SetPauseFlagToFalse()
    {
        pauseFlag = false;
    }

    public void SubmitButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("MenuConfirm");
    }
}
