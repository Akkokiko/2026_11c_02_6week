using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Player")]
    [SerializeField] private Player m_player;
    public Player Player => m_player;

    [Header("Checkpoints")]
    public CheckpointFlag[] m_checkpointFlags;
    private CheckpointFlag m_activeCheckpointFlag;

    [SerializeField] private CheckpointFlag m_startCheckpointFlag;

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
    }

    public void Start()
    {
        m_player.m_OnPlayerDeath.AddListener(respawn);

        LoadGame();
    }

    public void activateCheckpointFlag(CheckpointFlag flag)
    {
        SetActiveCheckpoint(flag);

        SaveGame();
    }

    private void SetActiveCheckpoint(CheckpointFlag flag)
    {
        foreach (var c in m_checkpointFlags)
        {
            c.deactivateCheckpointFlag();
        }

        m_activeCheckpointFlag = flag;
        flag.activateCheckpointFlag();
    }

    private void SaveGame()
    {
        int coins = m_player.PlayerInventory.m_coinAmount;

        SaveController.Instance.SaveGame(
            coins,
            m_activeCheckpointFlag.CheckpointID
        );
    }

    private void LoadGame()
    {
        SaveData data = SaveController.Instance.LoadGame();

        if (data != null)
        {
            m_player.PlayerInventory.setCoinAmount(data.CoinAmount);

            foreach (var checkpoint in m_checkpointFlags)
            {
                if (checkpoint.CheckpointID == data.CheckpointID)
                {
                    SetActiveCheckpoint(checkpoint);

                    Debug.Log("Przywrócono checkpoint: " + data.CheckpointID);
                    return;
                }
            }

            Debug.LogWarning("Nie znaleziono zapisanego checkpointa.");
        }

        // Brak zapisu lub nieznany checkpoint
        SetActiveCheckpoint(m_startCheckpointFlag);
    }

    private void respawn(Player player)
    {
        player.transform.position = m_activeCheckpointFlag.transform.position;
    }
}
