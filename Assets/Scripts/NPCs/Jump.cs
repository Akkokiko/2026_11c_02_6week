using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Entity m_entity;
    public float jumpForce => m_entity.m_currentJumpForce;
    
    private LayerMask m_groundedMask;
    private bool m_isGrounded;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_groundedMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        m_isGrounded = Physics2D.Raycast(this.transform.position, Vector2.down, 0.65f * transform.localScale.x, m_groundedMask).collider != null;
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f && m_isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}