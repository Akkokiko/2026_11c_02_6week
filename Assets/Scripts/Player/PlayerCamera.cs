using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera m_camera;
    [SerializeField] private float m_stayingStillTime;
    [SerializeField] private float m_currStayingStilTime;
    private bool m_isPlayerStill;

    [SerializeField] private float m_maxCamDist;

    private Player m_Player;
    private Transform m_cursorTransform;
    void Start()
    {
        m_Player = LevelManager.instance.Player;
        m_currStayingStilTime = m_stayingStillTime;

        GameObject dummyGameObject = new GameObject();
        dummyGameObject.transform.position = m_Player.transform.position;

        m_cursorTransform = dummyGameObject.transform;
    }

    
    void FixedUpdate()
    {
        m_isPlayerStill = m_Player.PlayerRigidbody.linearVelocity.sqrMagnitude < 0.01f;
        if (!m_Player.IsInMenu && m_isPlayerStill && m_currStayingStilTime > 0)
        {
            m_currStayingStilTime -= Time.deltaTime;
        }
        else if (!m_Player.IsInMenu && m_isPlayerStill && m_currStayingStilTime <= 0)
        {
            var mousePos = Mouse.current.position.ReadValue();
            Vector3 targetDirection = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,0)) - m_Player.transform.position;

            m_cursorTransform.position = m_Player.transform.position + Vector3.ClampMagnitude(targetDirection, m_maxCamDist);

            m_camera.Follow = m_cursorTransform;
            m_camera.LookAt = m_cursorTransform;
        }
        else
        {
            if (m_currStayingStilTime != m_stayingStillTime)
            {
                m_camera.Follow = m_Player.transform;
                m_camera.LookAt = m_Player.transform;
            }
            m_currStayingStilTime = m_stayingStillTime;
        }
    }

    private void OnDestroy() //memory leak protection
    {
        if (m_cursorTransform != null)
        {
            Destroy(m_cursorTransform.gameObject);
        }
    }
}
