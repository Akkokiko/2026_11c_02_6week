using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Entity m_connectedEntity;
    [SerializeField] private Slider m_slider;

    void Start()
    {
        m_connectedEntity.m_OnEntityHealthChanged.AddListener(updateHealth);
        

    }

    private void updateHealth(Entity entity)
    {
        m_slider.value = (float)entity.CurrentHealth/entity.MaxHealth;
    }
}
