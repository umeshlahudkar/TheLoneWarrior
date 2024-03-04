using UnityEngine;
using UnityEngine.UI;

public class SettingScreen : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private bool hasVolumeInit = false;

    private void OnEnable()
    {
        hasVolumeInit = false;
        UpdateVolumeSlider();
    }

    private void UpdateVolumeSlider()
    {
        musicVolumeSlider.value = SoundManager.Instance.MusicVolume/SoundManager.Instance.MusicBaseVolume;
        soundVolumeSlider.value = SoundManager.Instance.SoundVolume/ SoundManager.Instance.SoundBaseVolume;

        hasVolumeInit = true;
    }

    public void OnMusicVolumeChanged()
    {
        if(hasVolumeInit)
        {
            SoundManager.Instance.ChangeMusicVolume(musicVolumeSlider.value);
        }
    }

    public void OnSoundVolumeChnaged()
    {
        if (hasVolumeInit)
        {
            SoundManager.Instance.ChangeSoundVolume(soundVolumeSlider.value);
        }
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
