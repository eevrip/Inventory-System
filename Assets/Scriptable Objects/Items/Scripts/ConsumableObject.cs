using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName  = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject
{
    public int food;
    public int water;
    public int health;
    public void Awake() {
        type = ItemType.Consumable;
    }
    public override void Use()
    {
        base.Use();
        //Consume item
        //Add food, water and health to current state
        PopUpMessagesManager.instance.ShowPopUpMessage("- " + title);
        PlayerStateManager.instance.onConsumableUpdate(this);
        //Remove from inventory
        //RemoveFromInventory();
    }
}
