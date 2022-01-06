using UnityEngine;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField]
    public GameObject gameObjectToTurnOff;
    [SerializeField]
    public GameObject gameObjectToTurnOn;

    public static float musicVolume { get; private set; }
    public static float soundsVolume { get; private set; }

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

    public void OptionsMusicSliderValueChange(float value)
    {
        musicVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OptionsSoundsSliderValueChange(float value)
    {
        soundsVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
    }
}
