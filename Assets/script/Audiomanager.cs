using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider bgmSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        // Mengatur slider ke nilai volume awal
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
}
