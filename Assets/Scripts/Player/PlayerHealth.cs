using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        
        if (currentHealth <= 0 || damage <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (currentHealth <= 0)
        {
            m_animator.SetBool("Dead", true);
            m_animator.SetTrigger("GotDeadlyHit");
            Die();
        }
        else{
            m_animator.SetTrigger("GotHit");
        }
    }

    private void Die()
    {
        
    }
}
