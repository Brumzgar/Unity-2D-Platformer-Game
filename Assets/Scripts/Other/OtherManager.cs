using System.Collections;
using System.Collections.Generic;
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
    }

    public void FixedUpdate()
    {       
        if (sceneName == "GameScene")
        {
            if (PlayerPrefs.GetString("showTutorials") == "OFF")
            {
                tutorialsGameObject.SetActive(false);
            } else
            {
                tutorialsGameObject.SetActive(true);
            }
        }
               
    }

    // do zrobienia - globalny manager zalezny od ticku boxa w ustawieniach zeby wlaczyc lub wylaczyc ca³kowicie dzwieki/muzyke
}
