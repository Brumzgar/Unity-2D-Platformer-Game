using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // List of sounds that we can use & mixers
    [SerializeField] public Sound[] sounds;
    [SerializeField] public AudioMixerGroup musicMixerGroup;
    [SerializeField] public AudioMixerGroup soundsMixerGroup;
    public MusicAndEffectsVolumeValueSaved musicAndEffectsVolumeValueSaved;
    public static AudioManager Instance;
    public bool pausedMenuMusicPlayed;
    string sceneName;

    void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            switch (s.audioType)
            {
                case Sound.AudioTypes.sounds:
                    s.source.outputAudioMixerGroup = soundsMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
                s.source.Play();

            if (s.loop)
                s.source.loop = true;
        }
    }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    IEnumerator UpdateMixerVolumeOnPausedGame()
    {
        while (true)
        {
            UpdateMixerVolume();
            yield return null;
        }
    }

    public IEnumerator PlayPausedGameMusicOnPausedGame()
    {
        while (true)
        {
            if (PauseMenuScript.gameIsPaused == true)
            {
                if (pausedMenuMusicPlayed == false)
                {
                    pausedMenuMusicPlayed = true;
                    FindObjectOfType<AudioManager>().Play("GamePausedMusic");
                }
            }
            else
            {
                if (sceneName != "GameOverScene")
                    FindObjectOfType<AudioManager>().Stop("GamePausedMusic");
                pausedMenuMusicPlayed = false;
            }
            yield return null;
        }
    }

    private void OnEnable()
    {
        musicAndEffectsVolumeValueSaved.MusicAndEffectsVolumeOnStart();
        StartCoroutine(UpdateMixerVolumeOnPausedGame());
        StartCoroutine(PlayPausedGameMusicOnPausedGame());
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.UnPause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void Loop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source.loop == false)
            s.source.loop = true;
    }

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("MixerMusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        soundsMixerGroup.audioMixer.SetFloat("MixerSoundsVolume", Mathf.Log10(PlayerPrefs.GetFloat("effectsVolume")) * 20);
    }
}
