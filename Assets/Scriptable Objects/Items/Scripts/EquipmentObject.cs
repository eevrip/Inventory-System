using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    //public int damage; //how much it damages the objects
    public int defence; //how much defense it offers

    public EquipmentKind equipmentKind;
    public void Awake() {
        type = ItemType.Equipment;
    }

    public override void Use() {
        base.Use();

        //Add items stats to character stats
        //EquipItem(); //Since it's static we can have access
                                                    

    }
    public override void EquipItem()
    {
        base.EquipItem();
        EquipmentManager.instance.AddStats(this);

    }
    public override void UnequipItem()
    {
        base.UnequipItem();
        EquipmentManager.instance.RemoveStats(this);
        
    }

}
public enum EquipmentKind{
        Head,
        Core,
        Legs,
        Feet
    }
