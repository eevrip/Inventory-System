using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    
    public GameObject recipeButtonGO;
  


    public GameObject craftingMenuPanel;

    public RecipesSubcategoryUI subcategoryPanel;
   

    
   

    public RecipePanelUI recipePanel;

    private bool isUIShown;
    public bool IsUIShown => isUIShown;
   
    public void OpenCraftingMenu()
    {
        isUIShown = true;
        UIManager.instance.MouseMovementEnabled();
        craftingMenuPanel.SetActive(true);
      
    }
    public void CloseCraftingMenu()
    {
        UIManager.instance.MouseMovementDisabled();
        CloseRecipePanel();
        CloseSubCategoryPanel(); 
        craftingMenuPanel.SetActive(false);
        isUIShown=false;
    }
    public void OpenSubCategoryPanel(int type)
    { //depending on Type

        subcategoryPanel.UpdatePanelDetails(type);
        
       
        subcategoryPanel.gameObject.SetActive(true);
    }
    
    public void CloseSubCategoryPanel()
    {
        subcategoryPanel.gameObject.SetActive(false);
    }
    public void OpenRecipePanel(Recipe recipe)
    {
        //depending on recipe
        recipePanel.UpdatePanelDetails(recipe);
        if (!recipePanel.gameObject.activeSelf)
        {
            recipePanel.gameObject.SetActive(true);
        }   
    }
    public void CloseRecipePanel()
    {
        recipePanel.gameObject.SetActive(false );
    }
    
    
}
