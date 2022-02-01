using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnEnable()
    {
        if (PlayerPrefs.GetString("showTutorials") != "OFF")
                PlayerPrefs.SetString("showTutorials", "ON");
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
