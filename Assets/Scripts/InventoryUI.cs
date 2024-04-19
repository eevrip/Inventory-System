using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryScreen;
    public GameObject InventoryScreen => inventoryScreen;
    [SerializeField] private Transform inventorySlotsParent;
    public Transform InventorySlotsParent => inventorySlotsParent;

    private bool isUIShown = false;
    public bool IsUIShown => isUIShown;

    private Inventory currentInventory;
    public Inventory CurrentInventory
    {
        get { return currentInventory; }
        set
        { currentInventory = value; }
    }

    private InventorySlot[] slots;
    public InventorySlot[] Slots { get { return slots; } set { slots = value; } }

    
    public virtual void Start()
    {
        slots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>(); //Get all the slots
      
    }



    public virtual void UpdateUI()
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

    public virtual void ShowInventory()
    {
       
        inventoryScreen.SetActive(true);
        isUIShown = true;
        
    }
    public virtual void CloseInventory()
    {
       
        inventoryScreen.SetActive(false);
        isUIShown = false;
        
    }
}
