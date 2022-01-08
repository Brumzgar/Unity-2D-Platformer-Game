using UnityEngine;

public class MenuSceneSoundScript : MonoBehaviour
{
    public bool inputFlag;
    public GameObject startingScreen;

    public void OnEnable()
    {
        Invoke("turnOnInputsAfterTime", 1);
        Invoke("PlayMusicAfterTime", 0.25f);
    }

    void Update()
    {
        if (MainMenuScript.gameIsStarting != true && inputFlag && !startingScreen.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                FindObjectOfType<AudioManager>().Play("MenuMove");
            }
        }
    }

    public void turnOnInputsAfterTime()
    {
        inputFlag = true;
    }

    public void SubmitButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("MenuConfirm");
    }

    public void PlayMusicAfterTime()
    {
        FindObjectOfType<AudioManager>().Play("MusicMenu");
    }
}
