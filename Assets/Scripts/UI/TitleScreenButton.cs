using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenButton : MonoBehaviour
{
    [SerializeField] private GameObject m_connectedMenu;
    [SerializeField] private Button m_selfButton;

    public void goToScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void toggleConnectedMenu(){
        m_connectedMenu.SetActive(!m_connectedMenu.activeSelf);
    }

    public void QuitGame()
    {
        if(Application.platform != RuntimePlatform.WebGLPlayer)
        {
            Application.Quit();
        }
    }
}
