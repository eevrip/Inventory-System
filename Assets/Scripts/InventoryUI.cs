using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{   [SerializeField]private GameObject inventoryScreen;
    public GameObject InventoryScreen => inventoryScreen;
    [SerializeField]private Transform inventorySlotsParent;
    public Transform InventorySlotsParent => inventorySlotsParent;
   
    private bool isUIShown = false;
    public bool IsUIShown => isUIShown;
   
    // Start is called before the first frame update
   /* public virtual void Start()
    {
        slotsUI = inventorySlotsParent.GetComponentsInChildren<InventorySlot>(); //Get all the slots
       
    }
   */
    

    public virtual void UpdateUI()
    {
        
    }
  
    public virtual void ShowInventory()
    {
       // playerMvm.enabled = false;
        inventoryScreen.SetActive(true);
        isUIShown = true;
       // Cursor.lockState = CursorLockMode.Confined; //Mouse Cursor appears and cannot escape the screen boundaries Confined wasnt working properly
       // mouseCursor.enabled = false;
    }
    public virtual void CloseInventory() {
       // playerMvm.enabled = true;
        inventoryScreen.SetActive(false);
        isUIShown = false;
       // Cursor.lockState = CursorLockMode.Locked; //Mouse Cursor is locked to the center of the screen
       // mouseCursor.enabled = true;
    }
}
