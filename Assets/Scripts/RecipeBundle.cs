using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBundle : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipeList;
    public List<Recipe> RecipeList => recipeList;

    [SerializeField] private int recipeType;
    public int RecipeType => recipeType;

    public delegate void OnRecipeListUpdate();
    public OnRecipeListUpdate onRecipeListUpdateCallback;
    public void UnlockRecipe(Recipe recipe)
    {
        recipeList.Add(recipe);
        //Update the UI recipe list
        if(onRecipeListUpdateCallback != null)
            onRecipeListUpdateCallback.Invoke();
    }

}
