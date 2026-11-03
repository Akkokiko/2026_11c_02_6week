using UnityEngine;
using UnityEngine.Audio;

public class SettingsUI : MonoBehaviour
{
    
    [SerializeField] private AudioMixer m_audioMixer;

    public void setVolume(float volume)
    {
        m_audioMixer.SetFloat("masterVolume", volume);
    }

}
