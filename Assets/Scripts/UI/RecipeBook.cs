using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] private List<Recipe> m_RecipeList;
    [SerializeField] private GameObject recipeVisual;
    
    void Start()
    {
        showDiscoveredRecipes();
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
    
    public void showDiscoveredRecipes()
    {
        foreach (Recipe recipe in m_RecipeList)
        {
            if (recipe.HasBeenDiscovered && this.gameObject.transform.Find(recipe.name) == null)
            {
                GameObject newRecipeVisual = Instantiate(recipeVisual);
                newRecipeVisual.name = recipe.name;

                //setting images
                newRecipeVisual.transform.Find("PotionResult").GetComponent<Image>().sprite = recipe.RecipeResult.PotionSprite;

                for (int i = 0; i < recipe.NeededFlowers.Count; i++)
                {
                    newRecipeVisual.transform.Find("FlowerSlot"+(i+1)).GetComponent<Image>().sprite = recipe.NeededFlowers[i].sprite;
                }

                newRecipeVisual.transform.SetParent(this.transform, false);
            }
        }
    }
}
