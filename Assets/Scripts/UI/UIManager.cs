using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] private InventoryUI m_inventoryUI;
    public InventoryUI InventoryUI => m_inventoryUI;

    public void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }



}
