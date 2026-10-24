using System.Collections.Generic;
using LitMotion;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float m_requiredWeight;

    private List<Entity> m_entitiesOnThePlate = new List<Entity>();
    private bool isActive = false;
    
    
    public UnityEvent onActivation = new UnityEvent();
    public UnityEvent onDeactivation = new UnityEvent();
    
    
    private float m_currentWeight;

    private MotionHandle m_pressurePlateChange;


    [SerializeField] private Image m_pressurePlateFill;


    private void Start()
    {
        UpdatePressurePlate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Entity entity))
        {
    
            m_entitiesOnThePlate.Add(entity);
            entity.m_onWeightChanged.AddListener(UpdatePressurePlate);
            UpdatePressurePlate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Entity entity))
        {
            if (m_entitiesOnThePlate.Contains(entity))
            {
                m_entitiesOnThePlate.Remove(entity);
                entity.m_onWeightChanged.RemoveListener(UpdatePressurePlate);
                UpdatePressurePlate();
            }
        }
    }

    void UpdatePressurePlate()
    {
        float nextWeight = 0;
        m_entitiesOnThePlate.ForEach(x => nextWeight+= x.m_currentWeight);

        if (m_pressurePlateChange.IsActive())
        {
            m_pressurePlateChange.Cancel();
        }

        m_pressurePlateChange = LMotion.Create(m_currentWeight, nextWeight, 0.5f).Bind(x => m_pressurePlateFill.fillAmount =  (m_currentWeight = x)/m_requiredWeight);
        
        
        if ( nextWeight>= m_requiredWeight)
        {
            if (!isActive)
            {
                m_pressurePlateFill.color = Color.green;
                onActivation.Invoke();
                isActive = true;
            }
        }
        else
        {
            if (isActive)
            {
                onDeactivation.Invoke();
                isActive = false;
            }
            m_pressurePlateFill.color = Color.red;
        }
    }
}
