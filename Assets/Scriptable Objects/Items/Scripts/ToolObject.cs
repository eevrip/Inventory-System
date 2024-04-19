using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Object", menuName = "Inventory System/Items/Tool")]
public class ToolObject : ItemObject
{
    public int damage; //how much it damages the objects
   

    public ToolKind toolKind;
    public void Awake()
    {
        type = ItemType.Tool;
    }

    public override void Use()
    {
        base.Use();

        //Add items stats to character stats
        //EquipItem(); //Since it's static we can have access


    }
    public override void EquipItem()
    {
        base.EquipItem();
        ToolManager.instance.AddStats(this);

    }
    public override void UnequipItem()
    {
        base.UnequipItem();
        ToolManager.instance.RemoveStats(this);

    }

}
public enum ToolKind
{
    Axe,
    Pickaxe,
    Weapon
}
