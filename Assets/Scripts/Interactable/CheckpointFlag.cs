using UnityEngine;

public class CheckpointFlag : Interactable
{
    
    private bool m_isCheckpointActive = false;
    public bool IsCheckpointActive => m_isCheckpointActive;
    
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    
    public override void Interact(Player player)
    {
        LevelManager.instance.activateCheckpointFlag(this);
    }

    public void activateCheckpointFlag()
    {
        if (!m_isCheckpointActive)
        {
            m_isCheckpointActive = true;
            m_spriteRenderer.color = Color.green;
            
        }
    }

    public void deactivateCheckpointFlag()
    {
        m_isCheckpointActive = false;
        m_spriteRenderer.color = Color.white;
    }
    
    
}
