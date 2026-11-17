using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance { get; private set; }

    private SaveData m_saveData;
    public SaveData SaveData => m_saveData;

    [SerializeField] List<Recipe> m_allRecipes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        m_saveData = GetSaveDataFromFile();
        if (m_saveData == null)
        {
            m_saveData = new SaveData();
        }
    }

    public void SaveGame(int coinAmount, int diamondAmount, string checkpointID, int sceneId, List<Recipe> recipeList, List<string> collectedCoins)
    {
        m_saveData.CoinAmount = coinAmount;
        m_saveData.DiamondAmount = diamondAmount;

        m_saveData.CheckpointID = checkpointID;
        if (sceneId == -1 && SceneManager.sceneCount == 1)
        {
            m_saveData.m_CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            m_saveData.m_CurrentLevel = sceneId;
        }

        if (m_saveData.CollectedCoins == null || collectedCoins == null)
        {
            m_saveData.CollectedCoins = new List<string>();
        }

        m_saveData.m_recipeList = recipeList;

        SimpleSaveSystem.SaveXML(m_saveData);

        Debug.Log("Gra zapisana!");
    }

    public void LoadGame()
    {
        m_saveData = GetSaveDataFromFile();
        if (m_saveData == null)
        {
            Debug.Log("Save Data is null");
            return;
        }
        StartCoroutine(WaitForLoadedScene());

    }
    public IEnumerator WaitForLoadedScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_saveData.m_CurrentLevel);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f && !asyncLoad.isDone)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone) yield return null;

        Debug.Log("loaded.");
    }

    public SaveData GetSaveDataFromFile()
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

    public Recipe GetRecipeForName(string recipeName)
    {
        return m_allRecipes.Where(r => r.name == recipeName).FirstOrDefault();
    }
}
