using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StorageSlot : InventorySlot
{

    private PlayerInventory backpack;
    private StorageUI storageUI;
    public override void Start()
    {
        base.Start();

        backpack = PlayerInventory.instance;
        storageUI = UIManager.instance.Storage_UI;
    }
   
    public void SwitchContainers()
    {
        // Debug.Log("Switching containers");
        if (Item)
        {
            if (backpack.Add(Item))
                storageUI.CurrentInventory.Remove(Item);
        }
    }
    public override void Update()
    {
        if (IsOnSlot && !Item)
        {
            ToolTipManager.instance.HideToolTip();
        }
    }

}
