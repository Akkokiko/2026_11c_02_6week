using UnityEngine;

public class EffectBarUI : MonoBehaviour
{
   
   [SerializeField] private EffectCard m_effectCardPrefab;
   [SerializeField] private GameObject m_effectBarContent;



   public void addEffectCard(Effect effect)
   {
      EffectCard effectCard = Instantiate(m_effectCardPrefab);
      effectCard.transform.parent = m_effectBarContent.transform;
      effectCard.StartEffect(effect);
      
   }
}
   