using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public List<ItemObject> ingredients = new List<ItemObject>();
    public ItemObject resultItem;
    private List<Ingredient> ingredientList = new List<Ingredient>();



    //Return the required number of a specific item needed for this recipe
    public int CountItem(ItemObject currItem)
    {
        int count = ingredients.Count(item => item.ID == currItem.ID);
        return count;
    }
    public bool CanCraft(PlayerInventory inventory)
    {
        //if the ingredientList is empty, add ingredients
        if (ingredientList.Count == 0)
        {
            Debug.Log("ingr list empu");
            ingredients.Sort(ComparingID);
            int count = 1;
            for (int i = 0; i < ingredients.Count; i = i + count)
            {
                count = CountItem(ingredients[i]);
                Ingredient ingredient = new Ingredient();
                ingredient.amount = count;
                ingredient.item = ingredients[i];
                ingredientList.Add(ingredient);

                //Debug.Log("REcipe" + i + " " + count);
                // Debug.Log("ingred " + ingredient.item + " " + ingredient.amount);
            }
        }

        //Check if all the ingredients are in the player inventory
        foreach (Ingredient ingredient in ingredientList)
        {
            if (ingredient.amount > inventory.AmountOf(ingredient.item))
                return false;
        }
        return true;

    }
    public bool Craft(PlayerInventory inventory)
    {
        if (CanCraft(inventory))
        {
            foreach (Ingredient ingredient in ingredientList)
            {
                inventory.RemoveItems(ingredient.item, ingredient.amount);
            }
            inventory.Add(resultItem);
            return true;
        }
        return false;
    }
    private static int ComparingID(ItemObject item1, ItemObject item2)
    {
        if (item1.ID < item2.ID)
            return -1;
        else if (item1.ID > item2.ID)
            return 1;
        else
            return 0;


    }
}
public class Ingredient
{
    public int amount;
    public ItemObject item;
}
