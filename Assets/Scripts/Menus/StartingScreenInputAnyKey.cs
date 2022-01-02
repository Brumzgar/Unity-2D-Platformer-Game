using UnityEngine;

public class StartingScreenInputAnyKey : MonoBehaviour
{
    [SerializeField]
    public GameObject gameObjectToTurnOff;
    [SerializeField]
    public GameObject gameObjectToTurnOn;
    public bool inputFlag;
    public bool enterPressed;

    public void OnEnable()
    {
        inputFlag = false;
        enterPressed = false;
        Invoke("turnOnInputsAfterTime", 1);
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && inputFlag == true && enterPressed == false)
        {
            FindObjectOfType<AudioManager>().Play("MenuConfirm");
            enterPressed = true;
            if (gameObjectToTurnOff.gameObject.activeInHierarchy == true) 
            {
                gameObjectToTurnOff.gameObject.SetActive(false);
            }
            if (gameObjectToTurnOn.gameObject.activeInHierarchy == false)
            {
                gameObjectToTurnOn.gameObject.SetActive(true);
            }
        }
    }

    public void turnOnInputsAfterTime()
    {
        inputFlag = true;
    }
}
