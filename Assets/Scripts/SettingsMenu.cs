using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer _audioMixer;

    public void SetVolume (float volume)
    {
        _audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
