using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneSoundScript : MonoBehaviour
{
    public bool inputFlag;
    public GameObject startingScreen;
    public GameObject optionsMenu;
    public void OnEnable()
    {
        Invoke("turnOnInputsAfterTime", 1);
    }

    void Update()
    {
        if (MainMenuScript.gameIsStarting != true)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W)) && inputFlag && !startingScreen.activeInHierarchy)
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
}
