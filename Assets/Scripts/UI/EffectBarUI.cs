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

   public void removeAllEffectCards()
   {
      int childCount = m_effectBarContent.transform.childCount;
      for (int i = childCount - 1; i >= 0; i--)
      {
         Destroy(m_effectBarContent.transform.GetChild(i).gameObject);
      }
   }
}
   