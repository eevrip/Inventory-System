using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class RecipePanelUI : MonoBehaviour
{
    
    public TextMeshProUGUI recipeTitle;
    [SerializeField] private GameObject naEffectCraftButton;
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
        CanCraft();
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
      
        if (currentRecipe != null) {
            bool canCraft = currentRecipe.Craft(PlayerInventory.instance);
            if (!canCraft)
            {
               
                PopUpMessagesManager.instance.ShowPopUpMessage("Don't have enough ingredients") ;
            }
            
        }
            
        for (int i = 0; i < currentRecipe.Ingredients.Count; i++)
        {
            resourcePool[i].UpdateDetails(currentRecipe.Ingredients[i]);
           
        }
        CanCraft();
    }
    public void CanCraft()
    {
        
        if (currentRecipe != null)
        {
            
            if (!currentRecipe.CanCraft(PlayerInventory.instance))
            {
                naEffectCraftButton.gameObject.SetActive(true);
                
                //PopUpMessagesManager.instance.ShowPopUpMessage("Don't have enough ingredients");
            }
            else
                naEffectCraftButton.gameObject.SetActive(false);
        }
    }
}