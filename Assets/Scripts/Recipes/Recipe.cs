using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private Potion m_RecipeResult;
    public Potion RecipeResult => m_RecipeResult;
    [SerializeField] private List<Flower> m_NeededFlowers;
    public List<Flower> NeededFlowers => m_NeededFlowers;
    [SerializeField] private bool m_hasBeenDiscovered;
    public bool HasBeenDiscovered => m_hasBeenDiscovered;

    public bool doFlowersCraftThis(List<Flower> flowers)
    {
        if(m_NeededFlowers.Count != flowers.Count)
        {
             return false;
        }
        Dictionary<Flower, int> flowerAmountsRecipe = new Dictionary<Flower, int>();
        Dictionary<Flower, int> flowerAmountsGiven = new Dictionary<Flower, int>();

        for (int i = 0; i < flowers.Count; i++)
        {
            if (flowerAmountsRecipe.ContainsKey(m_NeededFlowers[i]))
            {
                flowerAmountsRecipe[m_NeededFlowers[i]] += 1;
            }
            else
            {
                flowerAmountsRecipe.Add(m_NeededFlowers[i], 1);
            }

            if (flowerAmountsGiven.ContainsKey(flowers[i]))
            {
                flowerAmountsGiven[flowers[i]] += 1;
            }
            else
            {
                flowerAmountsGiven.Add(flowers[i], 1);
            }
        }
        Debug.Log(flowerAmountsRecipe);
        Debug.Log(flowerAmountsGiven);
        //For each flower we just transcribed 
        foreach (Flower flower in flowerAmountsRecipe.Keys)
        {
            //check if both recipes match
            if (flowerAmountsGiven[flower] != flowerAmountsRecipe[flower])
            {
                return false;
            }
        }

        return true;
    }

    public void markAsDiscovered()
    {
        if (!m_hasBeenDiscovered)
        {
            m_hasBeenDiscovered = true;
        }
    }
}
