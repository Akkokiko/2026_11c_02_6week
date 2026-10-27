using UnityEngine;
using UnityEngine.UI;
using LitMotion;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Entity m_connectedEntity;
    [SerializeField] private Slider m_slider;
    [SerializeField] private float m_duration;

    private MotionHandle m_motionHandle;

    void Start()
    {
        if (m_connectedEntity == null)
        {
          m_connectedEntity = LevelManager.instance.Player;  
        }
        m_connectedEntity.m_OnEntityHealthChanged.AddListener(updateHealth);
    }

    private void updateHealth(Entity entity)
    {
        float targetValue = (float)entity.CurrentHealth/entity.MaxHealth;

        m_motionHandle.TryCancel();
        m_motionHandle = LMotion.Create(m_slider.value, targetValue, m_duration).WithEase(Ease.OutCubic).Bind(value => m_slider.value = value);
    }

    private void OnDestroy()
    {
        if (m_connectedEntity != null)
        {
            m_connectedEntity.m_OnEntityHealthChanged.RemoveListener(updateHealth);
        }
        
        m_motionHandle.TryCancel();
    }
}
