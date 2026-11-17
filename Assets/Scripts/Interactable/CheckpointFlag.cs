using Sound;
using UnityEngine;

public class CheckpointFlag : Interactable
{
    private static readonly int IsActiveHash = Animator.StringToHash("IsActive");
    private static readonly int IdleStateHash = Animator.StringToHash("Idle");
    private static readonly int DownStateHash = Animator.StringToHash("Down");

    [SerializeField] private string m_checkpointID;

    public string CheckpointID => m_checkpointID;

    private bool m_isCheckpointActive = false;
    public bool IsCheckpointActive => m_isCheckpointActive;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Sex m_checkpointSound;

    public override void Interact(Player player)
    {
        LevelManager.instance.activateCheckpointFlag(this);
    }

    public void activateCheckpointFlag(bool instant = false)
    {
        m_checkpointSound.Play(transform.position);
        m_isCheckpointActive = true;
        m_animator.SetBool(IsActiveHash, true);
        if (instant)
        {
            m_animator.Play(IdleStateHash, 0, 0f);
        }
    }

    public void deactivateCheckpointFlag(bool instant = false)
    {
        m_isCheckpointActive = false;
        m_animator.SetBool(IsActiveHash, false);
        if (instant)
        {
            m_animator.Play(DownStateHash, 0, 0f);
        }
    }
}
