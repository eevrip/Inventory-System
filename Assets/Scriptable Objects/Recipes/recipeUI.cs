using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recipeUI : MonoBehaviour
{
    public Recipe recipe;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
           // Debug.Log("Recipe hover over");
           // if (recipe.CanCraft(PlayerInventory.instance))
           // {
             //   Debug.Log("Can craft");
                recipe.Craft(PlayerInventory.instance);
           // }
           // else
            //    Debug.Log("Cannot craft");
        }
        //Check if can craft
        
    }
}
