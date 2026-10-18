using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] private List<Recipe> m_RecipeList;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Recipe evaluateRecipe(List<Flower> flowers)
    {
        List<Recipe> validRecipes = new List<Recipe>();
        foreach (Recipe recipe in m_RecipeList)
        {
            if (recipe.doFlowersCraftThis(flowers))
            {
                validRecipes.Add(recipe);
            }
        }

        if(validRecipes.Count != 1)
        {
            Debug.Log("Couldn't Evaluate Recipe: Valid Recipe Count =" + validRecipes.Count);
            return null;
        }
        else return validRecipes[0];
    }
}
