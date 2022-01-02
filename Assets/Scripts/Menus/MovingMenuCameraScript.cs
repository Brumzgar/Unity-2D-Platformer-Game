using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingMenuCameraScript : MonoBehaviour
{
    public float cameraSpeed;
    public Transform cameraPosition;
    public bool flag;
    public string sceneName;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        flag = true;
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnDisable()
    {
        flag = false;
    }

    private void Update()
    {
        if (flag == true)
        {
            transform.Translate(Vector2.right * cameraSpeed * Time.deltaTime);
            if (sceneName == "MenuScene")
            {
                if (cameraPosition.transform.position.x >= 58)
                {
                    cameraPosition.transform.localPosition = new Vector3(-54, 0, -10);
                }
            }
        }
    }
}
