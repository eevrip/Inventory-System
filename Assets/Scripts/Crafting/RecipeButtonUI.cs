using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class RecipeButtonUI : ToolTipSlot
{
    [SerializeField] private TextMeshProUGUI title;
   
    [SerializeField] private Image img;
    [SerializeField] private GameObject highlight;
    private Recipe recipe;
    public Button recipeButton;
    public Recipe ButtonRecipe => recipe;
    // Start is called before the first frame update
    void Start()
    {
       
        recipeButton.onClick.AddListener(() => OpenRecipePanel(recipe));
    }

    // Update is called once per frame
    public void UpdateDetails(Recipe recipe)
    {
        
        title.text = recipe.recipeName;
       
        img.sprite = recipe.resultItem.icon;
        this.recipe = recipe;
        Item = recipe.resultItem;
        
    }

    public void OpenRecipePanel(Recipe recipe)
    {
         UIManager.instance.Crafting_UI.OpenRecipePanel(recipe);
        ToolTipManager.instance.HideToolTip();
        highlight.SetActive(true);
    }
    public void DeactivateHighlight() { highlight.SetActive(false); }
}
