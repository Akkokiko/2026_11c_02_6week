using System;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Player")]
    [SerializeField] private Player m_player;
    public Player Player => m_player;

    [Header("Checkpoints")]
    private CheckpointFlag[] m_checkpointFlags;
    private CheckpointFlag m_activeCheckpointFlag;

    [SerializeField] private CheckpointFlag m_startCheckpointFlag;

    [Header("Audio")]
    [SerializeField] private Sex m_backgroundMusic;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        m_checkpointFlags = FindObjectsByType<CheckpointFlag>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }


    public void Start()
    {
        m_player.m_OnPlayerDeath.AddListener(respawn);

        m_backgroundMusic.Play(transform.position);

        LoadGame();
    }

    public void activateCheckpointFlag(CheckpointFlag flag)
    {
        SetActiveCheckpoint(flag);

        SaveGame();
    }

    private void SetActiveCheckpoint(CheckpointFlag flag, bool instant = false)
    {
        foreach (var c in m_checkpointFlags)
        {
            if (c != flag)
            {
                c.deactivateCheckpointFlag(instant);
            }
        }

        m_activeCheckpointFlag = flag;
        flag.activateCheckpointFlag(instant);
    }

    public void SaveGame()
    {
        int coins = m_player.PlayerInventory.m_coinAmount;
        int diamonds = m_player.PlayerInventory.m_diamondAmount;

        SaveController.Instance.SaveGame(
            coins,
            diamonds,
            m_activeCheckpointFlag.CheckpointID,
            -1,
            m_player.RecipeList,
            SaveController.Instance.SaveData.CollectedCoins
        );
    }

    public void GoToNextLevel()
    {
        int coins = m_player.PlayerInventory.m_coinAmount;
        int diamonds = m_player.PlayerInventory.m_diamondAmount;

        SaveController.Instance.SaveGame(
            coins,
            diamonds,
            m_activeCheckpointFlag.CheckpointID,
            SceneManager.GetActiveScene().buildIndex + 1,
            m_player.RecipeList,
            SaveController.Instance.SaveData.CollectedCoins
        );
        SaveController.Instance.LoadGame();
    }

    private void LoadGame()
    {
        SaveData data = SaveController.Instance.GetSaveDataFromFile();

        if (data != null)
        {
            m_player.PlayerInventory.setCoinAmount(data.CoinAmount);
            foreach (Recipe currRecipe in data.m_recipeList)
            {
                m_player.AddKnownRecipe(SaveController.Instance.GetRecipeForName(currRecipe.name));
                m_player.RecipeBook.showDiscoveredRecipes();
            }
            foreach (var checkpoint in m_checkpointFlags)
            {
                if (checkpoint.CheckpointID == data.CheckpointID)
                {
                    SetActiveCheckpoint(checkpoint, true);

                    Debug.Log("Przywrócono checkpoint: " + data.CheckpointID);
                    return;
                }
            }

            Debug.LogWarning("Nie znaleziono zapisanego checkpointa.");
        }

        SetActiveCheckpoint(m_startCheckpointFlag, true);
    }

    private void respawn(Player player)
    {
        player.transform.position = m_activeCheckpointFlag.transform.position;
    }
}
