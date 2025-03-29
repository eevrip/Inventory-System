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

    public GameObject textSubcategory;

    
   

    public RecipePanelUI recipePanel;

    private bool isUIShown;
    public bool IsUIShown => isUIShown;
    [SerializeField]
    private List<GameObject> mainMenuHighlights = new List<GameObject>();

    
    public void OpenCraftingMenu()
    {
        isUIShown = true; 
        DeactivateAllHighlights();
        UIManager.instance.MouseMovementEnabled();
        ToolTipManager.instance.DeactivateButtonsInfo();
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
       DeactivateAllHighlights();
        subcategoryPanel.UpdatePanelDetails(type);
         textSubcategory.SetActive(true);
       
        subcategoryPanel.gameObject.SetActive(true);
    }
    
    public void CloseSubCategoryPanel()
    {
        subcategoryPanel.gameObject.SetActive(false);
        textSubcategory.gameObject.SetActive(false);
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
    
    public void DeactivateAllHighlights()
    {
        foreach (GameObject highlight in mainMenuHighlights)
            highlight.SetActive(false);
    }
}
