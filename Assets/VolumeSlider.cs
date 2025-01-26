using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    private void Start()
    {
        float val;
        mixer.GetFloat("Volume", out val);
        slider.value = Mathf.Pow(10, val/20);
    }

    public void ChangeVolume(float volume)
    {
        mixer.SetFloat("Volume", Mathf.Log10(volume)*20);
        if (volume < 0.0002) mixer.SetFloat("Volume", -80);
    }
}
