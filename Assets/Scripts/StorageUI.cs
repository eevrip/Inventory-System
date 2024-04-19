using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUI : InventoryUI
{

    private Storage currentInventory;
    public Storage CurrentInventory { get { return currentInventory; } }
    private BackpackUI backpackUI;
    private StorageSlot[] slots;
    // Start is called before the first frame update
    public void Start()
    {


        slots = InventorySlotsParent.GetComponentsInChildren<StorageSlot>(); //Get all the slots
        
        backpackUI = UIManager.instance.Backpack_UI;
    }

    
    public void SetStorage(Storage storage)
    {
       
        currentInventory = storage;
        storage.onItemUpdateCallback += UpdateUI;
        UpdateUI();
    }
    public void ClearStorage()
    {
        
        currentInventory.onItemUpdateCallback -= UpdateUI;
        currentInventory = null;
        ClearUI();
    }
    public override void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        { //Always loop all slots
           if (i < currentInventory.InventoryContainer.Inventory.Count) //As long as there are items in our inventory container
            {
                slots[i].AddItem(currentInventory.InventoryContainer.Inventory[i]); //Add the items that exist in our inventory to the UI inventory
            }
            else
            { //The rest of the slots are empty
                slots[i].ClearSlot();
            }
        }
    }
    void ClearUI()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].ClearSlot();
    }
    public override void ShowInventory()
    {   backpackUI.InventoryMode(false);
        backpackUI.ShowInventory(); //need to call backpack inventory from here
       
        //inventoryScreen.SetActive(true);
       // isUIShown = true;
       base.ShowInventory();
       
    }
    public override void CloseInventory()
    {
        
        ClearStorage();
       base.CloseInventory();
       // inventoryScreen.SetActive(false);
       // isUIShown = false;
        
    }
}
