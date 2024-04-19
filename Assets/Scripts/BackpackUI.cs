using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : InventoryUI
{
   
    [SerializeField] private GameObject player;
 
    private PlayerMovement playerMvm;

    private PlayerInventory inventory;
   
    private Camera cam;
    private MouseLook mouseCursor;
  
   
    public override void Start()
    {
       
        base.Start();
        CurrentInventory = PlayerInventory.instance;
         CurrentInventory.onItemUpdateCallback += UpdateUI;
        playerMvm = player.GetComponent<PlayerMovement>();
        cam = Camera.main; //Camera
        mouseCursor = cam.GetComponent<MouseLook>(); //MouseLook script of the camera

    }



  
    public void InventoryMode(bool inventoryMode)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].UpdateMode(inventoryMode);
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
