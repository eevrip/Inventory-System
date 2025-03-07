using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipePanelUI : MonoBehaviour
{
    
    public TextMeshProUGUI recipePanelTxt;
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
        recipePanelTxt.text = recipe.recipeName;

        ShowResources(recipe);
    }

    public void ShowResources(Recipe recipe)
    {

        ResetResources();
        //activate the number of gameobjects equal to the length list
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            resourcePool[i].UpdateDetails(recipe.ingredients[i]);
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
        if(currentRecipe != null)
            currentRecipe.Craft(PlayerInventory.instance);
    }
}