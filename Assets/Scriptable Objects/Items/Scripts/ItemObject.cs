using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
     Resource,
    Consumable,
    Tool,
    Equipment,
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
    public bool canBeCrafted;
    public virtual void Use() {

        Debug.Log("Use " + prefab.name);
       // PopUpMessagesManager.instance.ShowPopUpMessage("- " + title);
    }
    public virtual void UnequipItem()
    {

        Debug.Log("Unequip " + prefab.name);
    }
    public virtual void EquipItem()
    {

        Debug.Log("Equip " + prefab.name);
    }
   
    //Removes item
    public void Remove() {

        PopUpMessagesManager.instance.ShowPopUpMessage("- " + title);
    }

    
}
