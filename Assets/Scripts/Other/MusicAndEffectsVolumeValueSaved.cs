using UnityEngine;

public class MusicAndEffectsVolumeValueSaved : MonoBehaviour
{
    public ChangeableSlidersViaInput musicSlider;
    public ChangeableSlidersViaInput effectsSlider;
    public AudioManager audioManager;

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
                musicSlider.sliderToAdjust.value = 0.4001f;
            }
            if (PlayerPrefs.GetFloat("effectsVolume") == 0.0001)
            {
                effectsSlider.sliderToAdjust.value = 0.6001f;
            }
        }
        else 
        {
            musicSlider.sliderToAdjust.value = PlayerPrefs.GetFloat("musicVolume");
            effectsSlider.sliderToAdjust.value = PlayerPrefs.GetFloat("effectsVolume");
        }

        PlayerPrefs.SetFloat("musicVolume", musicSlider.sliderToAdjust.value);
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.sliderToAdjust.value);
        audioManager.UpdateMixerVolume();
    }

    void Update()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.sliderToAdjust.value);
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.sliderToAdjust.value);
        audioManager.UpdateMixerVolume();
    }
}
