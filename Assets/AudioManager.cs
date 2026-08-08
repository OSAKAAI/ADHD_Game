using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;

public void StopMusic()
{
    if (musicSource.isPlaying)
        musicSource.Stop();
}

public void PlayMusic()
{
    if (!musicSource.isPlaying)
        musicSource.Play();
}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        musicSource.volume = volume;

        int musicOn = PlayerPrefs.GetInt("MusicOn", 1);
        musicSource.mute = (musicOn == 0);

        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void ToggleMusic(bool isOn)
    {
        musicSource.mute = !isOn;
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
    }
}