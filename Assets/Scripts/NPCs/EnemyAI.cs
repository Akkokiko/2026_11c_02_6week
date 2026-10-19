
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;

    [Header("Detection")]
    public Transform player;
    public float sightRange = 5f;
    public float attackRange = 1.2f;

    [Header("Combat")]
    public float chaseSpeed = 3f;
    public float attackCooldown = 1.5f;
    public float anticipationDuration = 0.6f;
    public float attackDuration = 0.5f;

    [Header("Ground Check")]
    public LayerMask floorLayer;
    public float groundCheckDistance = 0.3f;
    public float groundCheckForwardOffset = 0.05f;

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Collider2D bodyCollider;
    private Transform targetPoint;

    private float nextAttackTime;
    private bool chasing;
    private bool busy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        targetPoint = pointB;
    }

    private void Update()
    {
        if (player == null)
        {
            chasing = false;
            return;
        }

        float distance = Vector2.Distance(
            transform.position, player.position);

        chasing = distance <= sightRange && IsFacingPlayer();

        if (!busy && chasing && distance <= attackRange)
        {
            if (Time.time >= nextAttackTime)
                StartCoroutine(PerformAttack());
        }

        if (animator != null)
        {
            animator.SetBool(
                "IsMoving",
                !busy && Mathf.Abs(rb.linearVelocity.x) > 0.1f
            );
        }
    }

    private void FixedUpdate()
    {
        if (pointA == null || pointB == null)
            return;

        if (busy)
        {
            StopHorizontalMovement();
            return;
        }

        if (player != null && chasing)
        {
            float distance = Vector2.Distance(
                transform.position, player.position);

            if (distance > attackRange)
                MoveTowards(player.position.x, chaseSpeed);
            else
                StopHorizontalMovement();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (targetPoint == null)
            return;

        float difference =
            targetPoint.position.x - transform.position.x;

        if (Mathf.Abs(difference) < 0.1f)
        {
            targetPoint = targetPoint == pointA
                ? pointB : pointA;
            return;
        }

        float direction = Mathf.Sign(difference);

        if (!HasGroundAhead(direction))
        {
            StopHorizontalMovement();
            targetPoint = targetPoint == pointA
                ? pointB : pointA;

            return;
        }

        MoveTowards(targetPoint.position.x, patrolSpeed);
    }

    private void MoveTowards(float targetX, float speed)
    {
        float difference = targetX - transform.position.x;

        if (Mathf.Abs(difference) < 0.05f)
        {
            StopHorizontalMovement();
            return;
        }

        float direction = Mathf.Sign(difference);

        if (!HasGroundAhead(direction))
        {
            StopHorizontalMovement();
            return;
        }

        rb.linearVelocity = new Vector2(
            direction * speed, rb.linearVelocity.y);

        if (spriteRenderer != null)
            spriteRenderer.flipX = direction > 0;
    }

    private bool HasGroundAhead(float direction)
    {
        if (bodyCollider == null)
            return false;

        Bounds bounds = bodyCollider.bounds;

        float x = bounds.center.x +
                  direction * (bounds.extents.x +
                               groundCheckForwardOffset);

        float y = bounds.min.y + 0.05f;

        Vector2 origin = new Vector2(x, y);

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance,
            floorLayer
        );

        Debug.DrawRay(
            origin,
            Vector2.down * groundCheckDistance,
            hit.collider != null ? Color.green : Color.red
        );

        return hit.collider != null;
    }

    private void StopHorizontalMovement()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    private bool IsFacingPlayer()
    {
        if (player == null)
            return false;

        float difference =
            player.position.x - transform.position.x;

        bool facingRight =
            spriteRenderer == null || !spriteRenderer.flipX;

        return (difference > 0 && facingRight) ||
               (difference < 0 && !facingRight);
    }

    private IEnumerator PerformAttack()
    {
        busy = true;
        nextAttackTime = Time.time + attackCooldown;

        StopHorizontalMovement();

        if (animator != null)
            animator.SetTrigger("Charged");

        yield return new WaitForSeconds(anticipationDuration);

        if (animator != null)
            animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);

        busy = false;
    }
}
