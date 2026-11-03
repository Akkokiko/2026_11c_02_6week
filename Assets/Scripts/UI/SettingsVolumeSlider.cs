using System;
using UnityEngine;

public class SettingsVolumeSlider : MonoBehaviour
{
    [SerializeField] SettingsUI m_settingsUI;
    [SerializeField] string m_audioGroupName;
    public void setVolume(float volume)
    {
        m_settingsUI.AudioMixer.SetFloat(m_audioGroupName, volume);
    }
}
