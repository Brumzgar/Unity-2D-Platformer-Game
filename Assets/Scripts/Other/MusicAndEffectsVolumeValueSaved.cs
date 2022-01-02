using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicAndEffectsVolumeValueSaved : MonoBehaviour
{
    //public AudioMixer effectsAudioMixer;

    public ChangeableSlidersViaInput musicSlider;
    public ChangeableSlidersViaInput effectsSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;


        if (PlayerPrefs.GetInt("MenuSoundsMusicFlag") == 0) // && sceneName == "MenuScene" 
        {
            PlayerPrefs.SetInt("MenuSoundsMusicFlag", 1);
            if (PlayerPrefs.GetFloat("musicVolume") == 0)
            {
                musicSlider.musicSliderElementToAdjust.value = 5;
            }
            if (PlayerPrefs.GetFloat("effectsVolume") == 0)
            {
                effectsSlider.musicSliderElementToAdjust.value = 5;
            }
        } else 
        {
            musicSlider.musicSliderElementToAdjust.value = PlayerPrefs.GetFloat("musicVolume");
            effectsSlider.musicSliderElementToAdjust.value = PlayerPrefs.GetFloat("effectsVolume");
        }


    }

    void Update()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.musicSliderElementToAdjust.value);
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.musicSliderElementToAdjust.value);

        //SetEffectsVolume(PlayerPrefs.GetFloat("effectsVolume") * 10);
        //AudioListener.volume = PlayerPrefs.GetFloat("effectsVolume") / 10;
    }

/*    public void SetEffectsVolume (float mixerEffectsVolume)
    {
        effectsAudioMixer.SetFloat("GameEffectsMixerVolume", mixerEffectsVolume);
    }*/
}
