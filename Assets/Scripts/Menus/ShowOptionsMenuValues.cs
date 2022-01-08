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

    private void OnEnable()
    {
        if (sceneName == "GameScene")
        {
            StartCoroutine(ShowOptionsMenuValuesOnPausedGame());
        }
    }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "GameScene")
        {
            StartCoroutine(ShowOptionsMenuValuesOnPausedGame());
        }

        if (PlayerPrefs.GetInt("MenuTutorialsFlag") == 0) 
        {
            PlayerPrefs.SetInt("MenuTutorialsFlag", 1);
            tutorialSlider.value = tutorialSlider.maxValue;
        }
        else
        {
            if (PlayerPrefs.GetString("showTutorials") == "ON")
            {
                tutorialSlider.value = tutorialSlider.maxValue;
            }
            else
            {
                tutorialSlider.value = tutorialSlider.minValue;
            }
        }
    }
    IEnumerator ShowOptionsMenuValuesOnPausedGame()
    {
        while (true)
        {
            if (tutorialSlider.value == tutorialSlider.maxValue)
            {
                PlayerPrefs.SetString("showTutorials", "ON");
            }
            else
            {
                PlayerPrefs.SetString("showTutorials", "OFF");
            }

            tutorialValueText.GetComponent<Text>().text = PlayerPrefs.GetString("showTutorials");

            float PlayerPrefsMusicVolume = PlayerPrefs.GetFloat("musicVolume");
            float PlayerPrefsSoundsVolume = PlayerPrefs.GetFloat("effectsVolume");

            PlayerPrefsMusicVolume = Mathf.Round(PlayerPrefsMusicVolume * 10);
            PlayerPrefsSoundsVolume = Mathf.Round(PlayerPrefsSoundsVolume * 10);

            musicValueText.GetComponent<Text>().text = PlayerPrefsMusicVolume.ToString(); //PlayerPrefs.GetFloat("musicVolume").ToString();
            effectsValueText.GetComponent<Text>().text = PlayerPrefsSoundsVolume.ToString(); //PlayerPrefs.GetFloat("effectsVolume").ToString();

            yield return null;
        }
    }

    void FixedUpdate()
    {
        if (tutorialSlider.value == tutorialSlider.maxValue)
        {
            PlayerPrefs.SetString("showTutorials", "ON");    
        }
        else
        {
            PlayerPrefs.SetString("showTutorials", "OFF");
        }

        tutorialValueText.GetComponent<Text>().text = PlayerPrefs.GetString("showTutorials");

        float PlayerPrefsMusicVolume = PlayerPrefs.GetFloat("musicVolume");
        float PlayerPrefsSoundsVolume = PlayerPrefs.GetFloat("effectsVolume");

        PlayerPrefsMusicVolume = Mathf.Round(PlayerPrefsMusicVolume * 10);
        PlayerPrefsSoundsVolume = Mathf.Round(PlayerPrefsSoundsVolume * 10);

        musicValueText.GetComponent<Text>().text = PlayerPrefsMusicVolume.ToString(); //PlayerPrefs.GetFloat("musicVolume").ToString();
        effectsValueText.GetComponent<Text>().text = PlayerPrefsSoundsVolume.ToString(); //PlayerPrefs.GetFloat("effectsVolume").ToString();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MenuSoundsMusicFlag", 0);
        PlayerPrefs.SetInt("MenuTutorialsFlag", 0);
        PlayerPrefs.SetFloat("musicVolume", 0);
        PlayerPrefs.SetFloat("effectsVolume", 0);
    }
}
