using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject pressAnyKeyText;
    public bool registerInputs;
    public Animator transitionGameAnimator;
    public Animator playerJumping;

    public void Start()
    {
        FindObjectOfType<AudioManager>().Stop("GamePausedMusic");
        FindObjectOfType<AudioManager>().Play("BossDeath");
        registerInputs = false;
        Invoke("PlayEnemyDeathSound", 1.1f);
        Invoke("PlayerJumping", 2.5f);
        Invoke("TurnOnInputAnyKey", 5);
    }

    void Update()
    {
        if (Input.anyKey && registerInputs == true)
        {
            FindObjectOfType<AudioManager>().Play("MenuConfirm");
            registerInputs = false;
            transitionGameAnimator.SetTrigger("End");
            Invoke("LoadMenuScene", 2);
        }
    }

    public void TurnOnInputAnyKey()
    {
        FindObjectOfType<AudioManager>().Play("GamePausedMusic");
        pressAnyKeyText.SetActive(true);
        registerInputs = true;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void PlayerJumping()
    {
        playerJumping.SetTrigger("HappyJumping");
        FindObjectOfType<AudioManager>().Play("Congratulations");
    }

    public void PlayEnemyDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
    }
}
