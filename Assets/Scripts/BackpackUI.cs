using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : InventoryUI
{
   
    [SerializeField] private GameObject player;
    // public GameObject PlayerGO { set { player = value; } }
    private PlayerMovement playerMvm;

    private PlayerInventory inventory;
   // private InventorySlot[] slotsUI;
    private Camera cam;
    private MouseLook mouseCursor;
    private BackpackSlot[] slots;
   
    private PlayerInventory currentInventory;
    
    // Start is called before the first frame update
    public void Start()
    {
        currentInventory = PlayerInventory.instance;
        currentInventory.onItemUpdateCallback += UpdateUI; //update UI depending on the Inventory changes
        slots = InventorySlotsParent.GetComponentsInChildren<BackpackSlot>(); //Get all the slots
      
        playerMvm = player.GetComponent<PlayerMovement>();
        cam = Camera.main; //Camera
        mouseCursor = cam.GetComponent<MouseLook>(); //MouseLook script of the camera

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
    public void InventoryMode(bool inventoryMode)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateMode(inventoryMode);
        }
    }
    public override void ShowInventory()
    {
        base.ShowInventory();
        playerMvm.enabled = false;
        
        Cursor.lockState = CursorLockMode.Confined; //Mouse Cursor appears and cannot escape the screen boundaries Confined wasnt working properly
        mouseCursor.enabled = false;
    }
    public override void CloseInventory()
    {
        base.CloseInventory();
        playerMvm.enabled = true;
       
        Cursor.lockState = CursorLockMode.Locked; //Mouse Cursor is locked to the center of the screen
        mouseCursor.enabled = true;
    }
}
