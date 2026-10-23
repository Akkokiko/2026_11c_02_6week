
using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat")]
    public float attackCooldown = 1.5f;
    public float anticipationDuration = 0.6f;
    public float attackDuration = 0.5f;
    public EnemyAttackHitbox attackHitboxScript;

    private Animator animator;
    private PointPatrol patrol;

    private float nextAttackTime;
    private Coroutine attackCoroutine;

    public bool IsBusy { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        patrol = GetComponent<PointPatrol>();
    }

    public void TryStartAttack()
    {
        if (IsBusy || Time.time < nextAttackTime)
            return;

        attackCoroutine = StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        IsBusy = true;
        nextAttackTime = Time.time + attackCooldown;

        if (attackHitboxScript != null)
            attackHitboxScript.EndAttack();

        patrol.StopHorizontalMovement();

        if (animator != null)
            animator.SetTrigger("Charging");

        yield return new WaitForSeconds(anticipationDuration);

        if (animator != null)
            animator.SetTrigger("Attack");

        if (attackHitboxScript != null)
            attackHitboxScript.BeginAttack();

        yield return new WaitForSeconds(attackDuration);

        if (attackHitboxScript != null)
            attackHitboxScript.EndAttack();

        IsBusy = false;
        attackCoroutine = null;
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        IsBusy = false;

        if (attackHitboxScript != null)
            attackHitboxScript.EndAttack();
    }
}
