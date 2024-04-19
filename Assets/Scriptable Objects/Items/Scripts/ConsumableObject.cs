using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName  = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject
{
    public float food;
    public float water;
    public float health;
    public void Awake() {
        type = ItemType.Consumable;
    }
    public override void Use()
    {
        base.Use();
        //Consume item
        //Add food, water and health to current state
        PlayerStateManager.instance.UpdateState(this);
        //Remove from inventory
        RemoveFromInventory();
    }
}
