using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum AudioTypes { sounds, music}
    public AudioTypes audioType;

    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool playOnAwake;

    public bool loop;
}
