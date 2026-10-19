using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public int damage = 20;

    private bool canDealDamage;
    private bool alreadyHit;

    private void Awake()
    {
        canDealDamage = false;
    }

    public void BeginAttack()
    {
        alreadyHit = false;
        canDealDamage = true;

        Collider2D hitbox = GetComponent<Collider2D>();

        if (hitbox == null)
            return;

        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        Collider2D[] results = new Collider2D[20];
        int count = hitbox.Overlap(filter, results);

        for (int i = 0; i < count; i++)
        {
            if (results[i] != null)
                TryDealDamage(results[i]);
        }
    }

    public void EndAttack()
    {
        canDealDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDealDamage(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryDealDamage(other);
    }

    private void TryDealDamage(Collider2D other)
    {
        if (!canDealDamage || alreadyHit)
            return;

        if (!other.CompareTag("Player") &&
            !other.GetComponentInParent<PlayerHealth>()?.CompareTag("Player") == true)
            return;

        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

        if (health == null)
            return;

        health.TakeDamage(damage);
        alreadyHit = true;
    }
}
