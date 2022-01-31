using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherManager : MonoBehaviour
{
    string sceneName;
    public GameObject tutorialsGameObject;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(ShowTutorialsInGame());
    }

    IEnumerator ShowTutorialsInGame()
    {
        while (true)
        {
            if (sceneName == "GameScene")
            {
                if (PlayerPrefs.GetString("showTutorials") == "OFF")
                {
                    tutorialsGameObject.SetActive(false);
                }
                else
                {
                    tutorialsGameObject.SetActive(true);
                }
            }
            yield return null;
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MenuSoundsMusicFlag", 0);
        //PlayerPrefs.SetInt("MenuTutorialsFlag", 0);
        PlayerPrefs.SetFloat("musicVolume", 0);
        PlayerPrefs.SetFloat("effectsVolume", 0);
    }
}
