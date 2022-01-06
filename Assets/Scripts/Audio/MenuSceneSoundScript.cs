using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSceneSoundScript : MonoBehaviour
{
    public bool inputFlag;
    public GameObject startingScreen;
    public GameObject optionsMenu;
    public MusicAndEffectsVolumeValueSaved musicAndEffectsVolumeValueSaved;
    public OptionsMenuScript optionsMenuScript;
    public AudioManager audioManager;

    public void Awake()
    {
        Debug.Log("------------------Awake---------------------------");
        Debug.Log("PlayerPrefs.GetFloat(musicVolume) = " + PlayerPrefs.GetFloat("musicVolume"));
        Debug.Log("PlayerPrefs.GetFloat(effectsVolume) = " + PlayerPrefs.GetFloat("effectsVolume"));
        Debug.Log("OptionsMenuScript.musicVolume = " + OptionsMenuScript.musicVolume);
        Debug.Log("OptionsMenuScript.soundsVolume = " + OptionsMenuScript.soundsVolume);
    }

    public void OnEnable()
    {
        Invoke("turnOnInputsAfterTime", 1);
        Invoke("PlayMusicAfterTime", 0.25f);

        Debug.Log("------------------OnEnable---------------------------");
        Debug.Log("PlayerPrefs.GetFloat(musicVolume) = " + PlayerPrefs.GetFloat("musicVolume"));
        Debug.Log("PlayerPrefs.GetFloat(effectsVolume) = " + PlayerPrefs.GetFloat("effectsVolume"));
        Debug.Log("OptionsMenuScript.musicVolume = " + OptionsMenuScript.musicVolume);
        Debug.Log("OptionsMenuScript.soundsVolume = " + OptionsMenuScript.soundsVolume);
    }

    public void Start()
    {
        Debug.Log("------------------BeforeStart---------------------------");
        Debug.Log("PlayerPrefs.GetFloat(musicVolume) = " + PlayerPrefs.GetFloat("musicVolume"));
        Debug.Log("PlayerPrefs.GetFloat(effectsVolume) = " + PlayerPrefs.GetFloat("effectsVolume"));
        Debug.Log("OptionsMenuScript.musicVolume = " + OptionsMenuScript.musicVolume);
        Debug.Log("OptionsMenuScript.soundsVolume = " + OptionsMenuScript.soundsVolume);
        audioManager.SetMixerVolumeOnStart();
        Debug.Log("------------------Start---------------------------");
        Debug.Log("PlayerPrefs.GetFloat(musicVolume) = " + PlayerPrefs.GetFloat("musicVolume"));
        Debug.Log("PlayerPrefs.GetFloat(effectsVolume) = " + PlayerPrefs.GetFloat("effectsVolume"));
        Debug.Log("OptionsMenuScript.musicVolume = " + OptionsMenuScript.musicVolume);
        Debug.Log("OptionsMenuScript.soundsVolume = " + OptionsMenuScript.soundsVolume);
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
