using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicAndEffectsVolumeValueSaved : MonoBehaviour
{
    public ChangeableSlidersViaInput musicSlider;
    public ChangeableSlidersViaInput effectsSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    public void OnEnable()
    {
        MusicAndEffectsVolumeOnStart();
    }

    public void MusicAndEffectsVolumeOnStart()
    {

        if (PlayerPrefs.GetInt("MenuSoundsMusicFlag") == 0)
        {
            PlayerPrefs.SetInt("MenuSoundsMusicFlag", 1);
            if (PlayerPrefs.GetFloat("musicVolume") == 0.0001)
            {
                musicSlider.sliderToAdjust.value = 0.5001f;
            }
            if (PlayerPrefs.GetFloat("effectsVolume") == 0.0001)
            {
                effectsSlider.sliderToAdjust.value = 0.5001f;
            }
        }
        else 
        {
            musicSlider.sliderToAdjust.value = PlayerPrefs.GetFloat("musicVolume");
            effectsSlider.sliderToAdjust.value = PlayerPrefs.GetFloat("effectsVolume");
        }

        PlayerPrefs.SetFloat("musicVolume", musicSlider.sliderToAdjust.value);
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.sliderToAdjust.value);
    }

    void Update()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.sliderToAdjust.value);
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.sliderToAdjust.value);
    }
}
