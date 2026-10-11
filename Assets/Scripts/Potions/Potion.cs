using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private string m_potionName;


    public virtual void use(Player player)
    {
        
    }

    public virtual void Affect(Entity entity)
    {
        Debug.Log("Affected: " + entity.name);
    }

}
