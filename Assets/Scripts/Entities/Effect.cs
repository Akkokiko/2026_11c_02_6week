using UnityEngine;

public enum EffectType
{
    SPEED_UP,
    SLOW_UP,
    JUMP_BOOST,
    SIZE_UP,
    SIZE_DOWN,
}

public class Effect 
{
    private EffectType m_effectType { get; }
    public EffectType EffectType => m_effectType;
    
    float m_duration {get; set;}
    public float Duration => m_duration;
    
    private float m_strength { get; }
    public float Strength => m_strength;
    
    public Effect(EffectType effectType, float duration, float strength)
    {
        m_effectType = effectType;
        m_duration = duration;
        m_strength = strength;
    }

    public bool countdown (float deltaTime)
    {
        m_duration -= deltaTime;
        return m_duration <= 0;
    }
}
