using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class InventorySystem
{
    [SerializeField] private int inventorySize;
    public int InventorySize =>  inventorySize;

    [SerializeField] private List<ItemObject> inventory;
    public List<ItemObject> Inventory => inventory;


    public InventorySystem(int size)
    {
        inventory = new List<ItemObject>();
        inventorySize = size;
    }

    public bool AddToInventory(ItemObject item)
    {
        if(inventory.Count >= inventorySize)
        {
            Debug.Log("Inventory full");
            return false;
        }
       inventory.Add(item);
        inventory.Sort(ComparingID);
        return true;
    }

    public void RemoveFromInventory(int inventorySlot)
    {
        inventory.RemoveAt(inventorySlot);
    }

    public void RemoveFromInventory(ItemObject item)
    {
        inventory.Remove(item);
    }
    //Implementing our own Comparison<T>, to sort the elements inside the backpack
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
