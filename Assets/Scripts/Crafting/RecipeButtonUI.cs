using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class RecipeButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image img;
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
        
        text.text = recipe.recipeName;
        img.sprite = recipe.resultItem.icon;
        this.recipe = recipe;
    }

    public void OpenRecipePanel(Recipe recipe)
    {
         UIManager.instance.Crafting_UI.OpenRecipePanel(recipe);
    }

}
