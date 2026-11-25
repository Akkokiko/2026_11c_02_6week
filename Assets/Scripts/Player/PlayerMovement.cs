using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    private Rigidbody2D m_PlayerRigidbody;
    private Collider2D m_PlayerCollider;
    public Player Player => m_Player;
    [SerializeField] private Animator m_animator;
    [SerializeField] private SpriteRenderer m_spriteRenderer;


    [Header("Horizontal Movement")]
    private float m_moveSpeed => Player.m_currentMoveSpeed;
    private InputAction m_horizontalInput;

    [Header("Jump")]
    private float m_jumpHeight => Player.m_currentJumpForce;
    [SerializeField] private AnimationCurve m_jumpCurve;
    private InputAction m_jumpInput;
    private bool m_isGrounded;
    [SerializeField] private float m_coyoteTime;
    private float m_baseCoyoteTime;
    private bool m_isMidJump;
    private LayerMask m_groundedMask;
    [SerializeField] private float m_minJumpDuration;
    [SerializeField] private float m_maxJumpDuration;
    private float m_currJumpDuration;

    void Start()
    {
        m_horizontalInput = Player.InputActionAsset.FindAction("MoveHorizontal");
        m_jumpInput = Player.InputActionAsset.FindAction("Jump");

        m_PlayerRigidbody = Player.PlayerRigidbody;
        m_PlayerCollider = (Collider2D)Player.PlayerCollider;

        m_groundedMask = LayerMask.GetMask("Floor");
        m_currJumpDuration = 0;
        m_baseCoyoteTime = m_coyoteTime;
    }

    void Update()
    {
        Player.m_shouldBeMoving = !Player.IsInMenu;
        if (Player.IsInMenu)
        {
            return;
        }
        bool m_shouldBeGrounded = m_PlayerCollider.IsTouchingLayers(m_groundedMask) && Physics2D.Raycast(this.transform.position, m_PlayerRigidbody.gravityScale >= 0 ? Vector2.down : Vector2.up, 0.65f * transform.localScale.x, m_groundedMask).collider != null;

        if(m_shouldBeGrounded == false)
        {
            m_coyoteTime -= Time.deltaTime;
            m_isGrounded = true;
        }
        else
        {
            m_coyoteTime = m_baseCoyoteTime;
            m_isGrounded = true;
        }

        if(m_coyoteTime <= 0)
        {
            m_isGrounded = false;
        }

        if (m_isGrounded && m_jumpInput.WasPressedThisFrame())
        {
            m_isMidJump = true;
        }
        if (!m_jumpInput.IsPressed() && m_currJumpDuration > m_minJumpDuration)
        {
            m_isMidJump = false;
        }

        if (m_isMidJump && m_currJumpDuration <= m_maxJumpDuration)
        {
            m_currJumpDuration += Time.deltaTime;
        }
        else
        {
            m_isMidJump = false;
            m_currJumpDuration = 0;
        }

        m_animator.SetFloat("Speed", Mathf.Abs(m_PlayerRigidbody.linearVelocityX));
        m_animator.SetFloat("YVelocity", m_PlayerRigidbody.linearVelocityY);
        m_animator.SetBool("Grounded", m_isGrounded);

    }
    void FixedUpdate()
    {
        if (!Player.IsInMenu && Player.m_shouldBeMoving)
        {
            float currentVelocityX = m_PlayerRigidbody.linearVelocityX;
            float inputVelocityX = m_horizontalInput.ReadValue<float>() * m_moveSpeed;

            if (inputVelocityX >= 0)
            {
                m_spriteRenderer.flipX = false;
            }
            else
            {
                m_spriteRenderer.flipX = true;
            }

            if (m_PlayerRigidbody.gravityScale >= 0)
            {
                m_spriteRenderer.flipY = false;
            }
            else
            {
                m_spriteRenderer.flipY = true;
            }

            if (Mathf.Abs(currentVelocityX) > m_moveSpeed)
            {
                if (inputVelocityX == 0 || Mathf.Sign(inputVelocityX) != Mathf.Sign(currentVelocityX))
                {
                    m_PlayerRigidbody.linearVelocityX = Mathf.Lerp(currentVelocityX, 0, Time.deltaTime * 5);
                }
                else
                {
                    m_PlayerRigidbody.linearVelocityX = Mathf.Lerp(currentVelocityX, currentVelocityX + inputVelocityX / 5, Time.deltaTime);
                }
            }
            else
            {
                m_PlayerRigidbody.linearVelocityX = inputVelocityX;
            }


            if (m_isMidJump)
            {
                //if (Mathf.Abs(m_PlayerRigidbody.linearVelocityY) < m_jumpHeight * 1.15f) //1.15 for a slight over
                //{
                    float gravityScale = m_PlayerRigidbody.gravityScale >= 0 ? 1 : -1;

                    float jumpTime = Mathf.Clamp01(m_currJumpDuration/m_maxJumpDuration);

                    if (m_currJumpDuration > m_minJumpDuration)
                    {
                        m_PlayerRigidbody.linearVelocityY = gravityScale * m_jumpHeight * m_jumpCurve.Evaluate(jumpTime);
                    }
                    else
                    {
                       m_PlayerRigidbody.linearVelocityY = gravityScale * m_jumpHeight;
                    }
                //}

            }
        }
        else
        {
            m_PlayerRigidbody.linearVelocity = new Vector2(0, 0);
        }

    }
}