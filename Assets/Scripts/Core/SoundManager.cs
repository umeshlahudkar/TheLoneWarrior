using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;

    private readonly float soundBaseVolume = 1f;
    private readonly float musicBaseVolume = 0.3f;

    private void Awake()
    {
        ChangeMusicVolume(0.5f);
        ChangeSoundVolume(1);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float newVolume)
    {
        ChangeSourceVolume(soundBaseVolume, newVolume, soundSource);
    }

    public void ChangeMusicVolume(float newVolume)
    {
        ChangeSourceVolume(musicBaseVolume, newVolume, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, float newVolume, AudioSource source)
    {
        float finalVolume = newVolume * baseVolume;
        source.volume = finalVolume;
    }

    public float MusicVolume { get { return musicSource.volume; } }
    public float SoundVolume { get { return soundSource.volume; } }
    public float MusicBaseVolume { get { return musicBaseVolume; } }
    public float SoundBaseVolume { get { return soundBaseVolume; } }
}