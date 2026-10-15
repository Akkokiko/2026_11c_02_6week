using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] private InventoryUI m_inventoryUI;
    public InventoryUI InventoryUI => m_inventoryUI;
    
    [SerializeField] private EffectBarUI m_effectBarUI;
    public EffectBarUI EffectBarUI => m_effectBarUI;

    public void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }



}
