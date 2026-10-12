using UnityEngine;


[CreateAssetMenu(fileName = "Potion", menuName = "PotionTest")]
public class Potion : ScriptableObject
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
