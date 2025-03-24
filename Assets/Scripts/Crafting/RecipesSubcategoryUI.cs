using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

using UnityEngine;

public class RecipesSubcategoryUI : MonoBehaviour
{
    public TextMeshProUGUI subcategoryPanelTxt;
    public GameObject RecipeButtonPrefab;
    public List<RecipeButtonUI> buttonPool = new List<RecipeButtonUI>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdatePanelDetails(int type)
    {
        
        switch (type)
        {
            case (0)://Resource
                subcategoryPanelTxt.text = "Resource";
                break;
            case (1)://Consumable
                subcategoryPanelTxt.text = "Consumables";
                break;

            case (2)://Tools
                subcategoryPanelTxt.text = "Tools";
                break;
            case (3)://Equipment
                subcategoryPanelTxt.text = "Equipment";
                break;
            case (4)://Furniture
                subcategoryPanelTxt.text = "Furniture";
                break;


        }
        
        ShowRecipeBundle(type);
    }

    private void ShowRecipeBundle(int type)
    {
        ResetButtons();
        RecipeBundle bundle = RecipeSystem.instance.RecipeBundles[type];
        int buttonsTot = buttonPool.Count;
        int bundleTot = bundle.RecipeList.Count;
       
        if (buttonsTot < bundleTot)
        {
            for (int i = buttonsTot; i < bundleTot; i++)
            {
                //instantiate
                //recipeButtonGO = Instantiate()
                //add to the list of buttons
                //RecipeButtonUI temp = recipeButtonGO.GetComponent<RecipeButtonUI>();
                //buttonsPool.Add(temp);
            }


        }
        for (int i = 0; i < bundleTot; i++)
        {
           
            buttonPool[i].UpdateDetails(bundle.RecipeList[i]);
            buttonPool[i].gameObject.SetActive(true);
        }

    }
    private void ResetButtons()

    {
       
        foreach (var buttons in buttonPool)
        {
            buttons.DeactivateHighlight();
            buttons.gameObject.SetActive(false);
        }

    }

    public void DeactivateHighlight()
    {
        foreach (var buttons in buttonPool)
        {
            buttons.DeactivateHighlight();
           
        }

    }
}
