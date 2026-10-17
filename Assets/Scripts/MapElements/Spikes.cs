using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
  [SerializeField] private int m_spikesDamage;
  private List<Entity> hitEntities =  new List<Entity>();
  


  

  private void OnTriggerStay2D(Collider2D other)
  {
    Debug.Log("OnCollisionEnter");
    if (other.gameObject.TryGetComponent(out Entity entity))
    {
      if (!hitEntities.Contains(entity))
      {
        entity.takeDamage(m_spikesDamage);
        if (entity != null)
        {
          StartCoroutine(hitCooldown(entity));
        }
      }
    }
  }

  IEnumerator<WaitForSeconds> hitCooldown(Entity e)
  {
    hitEntities.Add(e);
    yield return new WaitForSeconds(1f);
    if (e != null)
    {
      hitEntities.Remove(e);
    }
  }
}
