using UnityEngine;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance { get; private set; }

    private SaveData m_saveData;

    private void Awake()
    {
        Instance = this;

        m_saveData = SimpleSaveSystem.LoadXML();

        if (m_saveData == null)
        {
            m_saveData = new SaveData();
        }

        if (m_saveData.CollectedCoins == null)
        {
            m_saveData.CollectedCoins = new List<string>();
        }
    }

    public void SaveGame(int coinAmount, string checkpointID)
    {
        m_saveData.CoinAmount = coinAmount;
        m_saveData.CheckpointID = checkpointID;

        SimpleSaveSystem.SaveXML(m_saveData);

        Debug.Log("Gra zapisana!");
    }

    public SaveData LoadGame()
    {
        return m_saveData;
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
