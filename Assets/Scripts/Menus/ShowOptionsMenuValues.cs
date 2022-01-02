using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowOptionsMenuValues : MonoBehaviour
{
    public Slider tutorialSlider;
    public GameObject tutorialValueText;
    public GameObject musicValueText;
    public GameObject effectsValueText;
    string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        Debug.Log("MenuTutorialsFlag = " + PlayerPrefs.GetInt("MenuTutorialsFlag") + " - before"); // test

        if (sceneName == "MenuScene" && PlayerPrefs.GetInt("MenuTutorialsFlag") == 0)
        {
            PlayerPrefs.SetInt("MenuTutorialsFlag", 1);
            tutorialSlider.value = 1;

            Debug.Log("MenuTutorialsFlag = " + PlayerPrefs.GetInt("MenuTutorialsFlag") + " - if"); // test
        }
        else
        {
            //tutorialValueText.GetComponent<Text>().text = PlayerPrefs.GetString("showTutorials");

            if (PlayerPrefs.GetString("showTutorials") == "ON")
            {
                tutorialSlider.value = 1;
            }
            else
            {
                tutorialSlider.value = 0;
            }

            Debug.Log("MenuTutorialsFlag = " + PlayerPrefs.GetInt("MenuTutorialsFlag") + " - else"); // test
        }

        Debug.Log("MenuTutorialsFlag = " + PlayerPrefs.GetInt("MenuTutorialsFlag") + " - after"); // test
    }

    void FixedUpdate()
    {
        if (tutorialSlider.value == 1)
        {
            PlayerPrefs.SetString("showTutorials", "ON");    
        }
        else
        {
            PlayerPrefs.SetString("showTutorials", "OFF");
        }

        tutorialValueText.GetComponent<Text>().text = PlayerPrefs.GetString("showTutorials");
        musicValueText.GetComponent<Text>().text = PlayerPrefs.GetFloat("musicVolume").ToString();
        effectsValueText.GetComponent<Text>().text = PlayerPrefs.GetFloat("effectsVolume").ToString();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MenuSoundsMusicFlag", 0);
        PlayerPrefs.SetInt("MenuTutorialsFlag", 0);
        PlayerPrefs.SetFloat("musicVolume", 0);
        PlayerPrefs.SetFloat("effectsVolume", 0);

        Debug.Log("MenuTutorialsFlag = " + PlayerPrefs.GetInt("MenuTutorialsFlag") + " - on quit"); // test
    }
}
