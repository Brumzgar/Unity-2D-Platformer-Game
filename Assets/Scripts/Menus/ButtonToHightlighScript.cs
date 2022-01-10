using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonToHightlighScript : MonoBehaviour
{
    [SerializeField]
    public GameObject buttonToHighlightOnStart;
    [SerializeField]
    public GameObject buttonToHighlightOnCancel;
    public bool flag;
    string sceneName;

    private void Start()
    {
        flag = true;
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnEnable()
    {
        flag = true;
    }

    private void OnDisable()
    {
        flag = false;
    }

    private void Update()
    {
        if (flag == true)
        {
            if (EventSystem.current.currentSelectedGameObject == buttonToHighlightOnStart)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
            EventSystem.current.SetSelectedGameObject(buttonToHighlightOnStart);
            flag = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
        {
            if (MainMenuScript.gameIsStarting == true)
            {
                EventSystem.current.SetSelectedGameObject(buttonToHighlightOnStart);
            } else if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(buttonToHighlightOnStart);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Mouse3) || Input.GetKeyDown(KeyCode.Mouse4) || Input.GetKeyDown(KeyCode.Mouse5) || Input.GetKeyDown(KeyCode.Mouse6))
        {
            GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(selectedGameObject);

            if (EventSystem.current.currentSelectedGameObject == null)
                flag = true;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            EventSystem.current.SetSelectedGameObject(buttonToHighlightOnCancel);
            if (sceneName == "MenuScene" && MainMenuScript.gameIsStarting == false)
                FindObjectOfType<AudioManager>().Play("PauseMenuDisabled");
        }
    }
}
