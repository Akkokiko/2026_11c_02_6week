using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using System.Linq;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer m_audioMixer;
    public AudioMixer AudioMixer => m_audioMixer;

    [Header("Resolutions")]
    private Resolution[] m_resolutions;
    [SerializeField] private TMP_Dropdown m_resDropdown;

    void Start()
    {
        m_resolutions = Screen.resolutions;

        m_resDropdown.ClearOptions();
        m_resDropdown.AddOptions(m_resolutions.Select(res => res.width+" x "+res.height).ToList());
        string currRes = Screen.currentResolution.width + " x " + Screen.currentResolution.height;
        int defaultRes = m_resDropdown.options.FindIndex(op => op.text == Screen.currentResolution.width + " x " + Screen.currentResolution.height);

        m_resDropdown.value = defaultRes;
        m_resDropdown.RefreshShownValue();
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
