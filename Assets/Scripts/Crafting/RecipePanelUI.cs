using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipePanelUI : MonoBehaviour
{
    
    public TextMeshProUGUI recipeTitle;
    [SerializeField] private TextMeshProUGUI description;
    public List<ResourceSlotUI> resourcePool = new List<ResourceSlotUI>();
    private Recipe currentRecipe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void UpdatePanelDetails(Recipe recipe)
    {
        currentRecipe = recipe;
        recipeTitle.text = recipe.recipeName;
        description.text = recipe.resultItem.description;
        ShowResources(recipe);
    }

    public void ShowResources(Recipe recipe)
    {

        ResetResources();
        //activate the number of gameobjects equal to the length list
        for (int i = 0; i < recipe.Ingredients.Count; i++)
        {
            resourcePool[i].UpdateDetails(recipe.Ingredients[i]);
            resourcePool[i].gameObject.SetActive(true);
        }

    }
    public void ResetResources()
    {
        foreach (var resource in resourcePool)
        {
            resource.gameObject.SetActive(false);
        }
    }
    public void Craft()
    {
        bool canCraft = false;
        if (currentRecipe != null) {
            canCraft = currentRecipe.Craft(PlayerInventory.instance);
            if (!canCraft)
            {
                Debug.Log("Dont have enough ingredients");
            }
        }
            
        for (int i = 0; i < currentRecipe.Ingredients.Count; i++)
        {
            resourcePool[i].UpdateDetails(currentRecipe.Ingredients[i]);
           
        }
    }
}