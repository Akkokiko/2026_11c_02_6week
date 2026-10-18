using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Header("Player")]
    [SerializeField] private Player m_player;
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
    }

    public void Start()
    {
        activateCheckpointFlag(m_startCheckpointFlag);
        m_player.m_OnPlayerDeath.AddListener(respawn);
    }

    public void activateCheckpointFlag(CheckpointFlag flag)
    {
        foreach (var c in m_checkpointFlags)
        {
            c.deactivateCheckpointFlag();
        }
        m_activeCheckpointFlag = flag;
        flag.activateCheckpointFlag();
        
    }

    void respawn(Player player)
    {
        player.transform.position = m_activeCheckpointFlag.transform.position;
    }


}
