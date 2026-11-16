using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenButton : MonoBehaviour
{
    [SerializeField] private GameObject m_connectedMenu;
    [SerializeField] private Button m_selfButton;

    public void NewGame()
    {
        SaveController.Instance.SaveGame(0,0, "00", 1, new List<Recipe>());

        SaveController.Instance.LoadGame();
    }

    public void toggleConnectedMenu()
    {
        m_connectedMenu.SetActive(!m_connectedMenu.activeSelf);
    }

    public void QuitGame()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            Application.Quit();
        }
    }
}
