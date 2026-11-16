using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance { get; private set; }

    private SaveData m_saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
        m_saveData = GetSaveData();
        if (m_saveData == null)
        {
            m_saveData = new SaveData();
        }
    }

    public void SaveGame(int coinAmount, string checkpointID, int sceneId)
    {
        m_saveData.CoinAmount = coinAmount;
        m_saveData.CheckpointID = checkpointID;
        if (sceneId == -1 && SceneManager.sceneCount == 1)
        {
            m_saveData.m_CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        }
        if (m_saveData.CollectedCoins == null)
        {
            m_saveData.CollectedCoins = new List<string>();
        }

        SimpleSaveSystem.SaveXML(m_saveData);

        Debug.Log("Gra zapisana!");
    }

    public void LoadGame()
    {
        m_saveData = GetSaveData();
        if (m_saveData == null)
        {
            Debug.Log("Save Data is null");
        }
        StartCoroutine(WaitForLoadedScene());

    }
    public IEnumerator WaitForLoadedScene()
    {
        Debug.Log("Rozpoczęto asynchroniczne ładowanie poziomu...");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_saveData.m_CurrentLevel);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f && !asyncLoad.isDone)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone) yield return null;

        Debug.Log("Poziom został pomyślnie załadowany.");
    }

    public SaveData GetSaveData()
    {
        return SimpleSaveSystem.LoadXML();
    }

    public bool IsCoinCollected(string coinID)
    {
        return m_saveData.CollectedCoins.Contains(coinID);
    }

    public void RegisterCoinCollected(string coinID)
    {
        if (!m_saveData.CollectedCoins.Contains(coinID))
        {
            m_saveData.CollectedCoins.Add(coinID);
        }
        Debug.Log("Zarejestrowano zebraną monetę: " + coinID);
    }
}
