using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/UniquePotions/ExplosionPotion")]
public class ExplosionPotion : Potion
{
    [Header("Effect")]
    [SerializeField] private float m_distance;
    private bool m_isExplosive;

    void Awake()
    {
        if (this.m_effectType == EffectType.EXPLODE)
        {
            m_isExplosive = true;
        }
        else if (this.m_effectType == EffectType.IMPLODE)
        {
            m_isExplosive = false;
        }
        else
        {
            Debug.LogError("Explosive Potion without proper effect type exists:" + this.name);
        }
    }

    public override void Affect(Entity entity)
    {
        Vector2 entitiesPosition = entity.gameObject.transform.position;

        float explosionStrength = (m_isExplosive ? 1 : -1) * this.Strength;
        float explosionRadius = m_distance;

        LayerMask layerMask = ~(1 << LayerMask.NameToLayer("UI"));

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(entitiesPosition, explosionRadius, layerMask))
        {
            if (hit.attachedRigidbody != null)
            {
                Vector2 hitPos = hit.attachedRigidbody.position;

                Vector2 expDirection = hitPos - entitiesPosition;
                float distanceToExp = expDirection.magnitude;

                expDirection = expDirection.normalized;

                float lossFactor = Mathf.Clamp01((explosionRadius - distanceToExp) / explosionRadius);

                Vector2 expForce = expDirection * explosionStrength * lossFactor;

                hit.attachedRigidbody.AddForce(expForce, ForceMode2D.Impulse);
            }

        }
    }
}