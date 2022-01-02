using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject pressAnyKeyText;
    public bool registerInputs;
    public Animator transitionGameAnimator;
    public Animator playerJumping;

    public void OnEnable()
    {
        Invoke("TurnOnInputAnyKey", 5);
        registerInputs = false;
        Invoke("PlayerJumping", 2.5f);
    }

    void Update()
    {
        if (Input.anyKey && registerInputs == true)
        {
            registerInputs = false;
            transitionGameAnimator.SetTrigger("End");
            Invoke("LoadMenuScene", 2);
        }
    }

    public void TurnOnInputAnyKey()
    {
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
    }
}
