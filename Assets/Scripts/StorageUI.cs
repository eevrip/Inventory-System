using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUI : InventoryUI
{

    private BackpackUI backpackUI;

    public override void Start()
    {

        Slots = InventorySlotsParent.GetComponentsInChildren<StorageSlot>(); //Get all the slots

        backpackUI = UIManager.instance.Backpack_UI;
    }


    public void SetStorage(Storage storage)
    {

        CurrentInventory = storage;
        storage.onItemUpdateCallback += UpdateUI;
        UpdateUI();
    }
    public void ClearStorage()
    {

        CurrentInventory.onItemUpdateCallback -= UpdateUI;
        CurrentInventory = null;
        ClearUI();
    }

    void ClearUI()
    {
        for (int i = 0; i < Slots.Length; i++)
            Slots[i].ClearSlot();
    }
    public override void ShowInventory()
    {
        backpackUI.InventoryMode(false);
        backpackUI.ShowInventory(); //need to call backpack inventory from here  
        base.ShowInventory();

    }
    public override void CloseInventory()
    {

        ClearStorage();
        base.CloseInventory();
    }
}
