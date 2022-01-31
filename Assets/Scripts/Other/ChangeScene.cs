using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnEnable()
    {
        /*if (PlayerPrefs.GetInt("MenuTutorialsFlag") == 0)
        {
            PlayerPrefs.SetInt("MenuTutorialsFlag", 1);*/
        Debug.Log("----------------ChangeScene OnEnable > BEFORE--------------------");
        Debug.Log(PlayerPrefs.GetString("showTutorials") + " = showTutorials");

        if (PlayerPrefs.GetString("showTutorials") != "OFF")
                PlayerPrefs.SetString("showTutorials", "ON");

        Debug.Log("----------------ChangeScene OnEnable > AFTER--------------------");
        Debug.Log(PlayerPrefs.GetString("showTutorials") + " = showTutorials");
        //}
        Invoke("ChangeSceneAfterTime", 8);
    }

    public void ChangeSceneAfterTime()
    {
        if (PlayerPrefs.GetString("showTutorials") == "ON")
        {
            SceneManager.LoadScene("BossTutorialScene");
        } 
        else
        {
            SceneManager.LoadScene("BossScene");
        }          
    }
}
