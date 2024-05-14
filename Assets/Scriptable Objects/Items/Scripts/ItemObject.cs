using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    Consumable,
    Equipment,
    Resource,
    Tool,
   Furniture
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public string title;
    [TextArea(15, 20)]
    public string description;
    public Sprite icon;
    public int ID = -1;
    
    public virtual void Use() {

        Debug.Log("Use " + prefab.name);
    }
    public virtual void UnequipItem()
    {

        Debug.Log("Unequip " + prefab.name);
    }
    public virtual void EquipItem()
    {

        Debug.Log("Equip " + prefab.name);
    }
    //Removes from inventory/toolbar. This doesn't include spawning the item into the world
    public void RemoveFromInventory() {
       // Inventory.instance.Remove(this);
    }

    
}
