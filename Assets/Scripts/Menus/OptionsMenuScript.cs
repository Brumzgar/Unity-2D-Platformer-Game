using UnityEngine;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField]
    public GameObject gameObjectToTurnOff;
    [SerializeField]
    public GameObject gameObjectToTurnOn;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameObjectToTurnOff.gameObject.SetActive(false);
            gameObjectToTurnOn.gameObject.SetActive(true);
        }
    }

    public void OnDisable()
    {
        FindObjectOfType<AudioManager>().Play("PauseMenuDisabled");
    }
 
}
