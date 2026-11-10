using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private Entity m_entity;
    public float jumpForce => m_entity.m_currentJumpForce;

    [Header("Animation")]
    [SerializeField] private Animator m_animator;

    [Header("Timing")]
    [SerializeField] private float m_landingCooldown = 0.25f;
    [SerializeField] private float m_anticipationDuration = 0.4f;

    private LayerMask m_groundedMask;
    private bool m_isGrounded;
    private bool m_wasGrounded;
    private bool m_isPreparingJump;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_groundedMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        m_isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            0.65f * transform.localScale.x,
            m_groundedMask
        ).collider != null;

        if (m_isGrounded && !m_wasGrounded)
        {
            m_animator.SetTrigger("Landing");
            StartCoroutine(PrepareNextJump());
        }

        m_animator.SetBool(
            "IsFalling",
            rb.linearVelocity.y < -0.01f && !m_isGrounded
        );

        m_wasGrounded = m_isGrounded;
    }

    private IEnumerator PrepareNextJump()
    {
        m_isPreparingJump = true;

        yield return new WaitForSeconds(m_landingCooldown);

        m_animator.SetTrigger("Anticipation");

        yield return new WaitForSeconds(m_anticipationDuration);

        if (m_isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            m_animator.SetTrigger("Jump");
        }

        m_isPreparingJump = false;
    }
}