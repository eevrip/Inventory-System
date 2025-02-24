using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    private List<Recipe> allRecipes;
    public List<Recipe> AllRecipes => allRecipes;


    public GameObject recipeButtonGO;
    public GameObject resourceSlotGO;


    public GameObject mainMenuPanel;

    public GameObject subcategoryPanel;
   

    private TextMeshProUGUI subcategoryPanelTxt;
    private List<Recipe> subcategoryPanelRecipes = new List<Recipe>();

    public GameObject recipePanel;
    private TextMeshProUGUI recipePanelTxt;
    private List<ItemObject> recipePanelIngredients = new List<ItemObject>();

    // Start is called before the first frame update
    void Start()
    {
        subcategoryPanelTxt = subcategoryPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenCraftingMenu()
    {
        mainMenuPanel.SetActive(true);
    }
    public void CloseCraftingMenu()
    {
        mainMenuPanel.SetActive(false);
        CloseRecipePanel();
        CloseSubCategoryPanel();
    }
    public void OpenSubCategoryPanel(int type)
    {
        switch (type)
        {
            case (0)://Consumable
                subcategoryPanelTxt.text = "Consumables";
                break;
            case (1)://Equipment
                subcategoryPanelTxt.text = "Equipment";
                break;
                
            case (2)://Resource
                subcategoryPanelTxt.text = "Resources";
                break;
            case (3)://Tool
                subcategoryPanelTxt.text = "Tools";
                break;
            case (4)://Furniture
                subcategoryPanelTxt.text = "Furniture";
                break;

        }
        //depending on Type
        subcategoryPanel.SetActive(true);
    }
    public void CloseSubCategoryPanel()
    {
        subcategoryPanel.SetActive(false);
    }
    public void OpenRecipePanel()
    {
        //depending on recipe
        recipePanel.SetActive(true);    
    }
    public void CloseRecipePanel()
    {
        recipePanel.SetActive(false );
    }
    public void Craft(Recipe recipe)
    {
        recipe.Craft(PlayerInventory.instance);
    }
}
