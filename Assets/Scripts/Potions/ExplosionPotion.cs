using Unity.VisualScripting;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/UniquePotions/ExplosionPotion")]
public class ExplosionPotion : Potion
{
    [Header("Effect")]
    [SerializeField] private float m_distance;

    [SerializeField] private ExplosionEffect m_explosionPrefab;
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

        foreach (Rigidbody2D hit in Physics2D.OverlapCircleAll(entitiesPosition, explosionRadius, layerMask).Where(col => col.attachedRigidbody != null).Select(col => col.attachedRigidbody))
        {
            Vector2 hitPos = hit.position;

            Vector2 expDirection = hitPos - entitiesPosition;
            float distanceToExp = expDirection.magnitude;
            if (distanceToExp > m_distance)
            {
                continue;
            }

            expDirection = expDirection.normalized;

            float lossFactor = Mathf.Clamp01((explosionRadius - distanceToExp) / explosionRadius);

            Vector2 expForce = expDirection * explosionStrength * lossFactor;
            expForce.y *= 1.2f;

            hit.linearVelocity = new Vector2(0,0);
            hit.AddForce(expForce, ForceMode2D.Impulse);

        }
        SpawnExplosion(entity.gameObject.transform.position);
    }

    void SpawnExplosion(Vector3 position)
    {
        var ex = Instantiate(m_explosionPrefab,position,Quaternion.identity);
        ex.Puff(m_distance);
    }
}