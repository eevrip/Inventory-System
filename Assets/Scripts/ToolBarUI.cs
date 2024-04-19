using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarUI : MonoBehaviour
{
    [SerializeField] private GameObject toolBarSlotsParent;
    
    private ToolBarSlot[] slots;
    private PlayerInventory backpack;
    private BackpackUI backpackUI;
    private int selectIdx = -1;
    private bool isToolBarEnabled;
    public bool IsToolBarEnabled { get { return isToolBarEnabled; }  }
   
    public void Start()
    {
        
       
       backpack = PlayerInventory.instance;
        backpack.onItemUpdateCallback += UpdateToolBar; //update UI depending on the Inventory changes
        
        slots = toolBarSlotsParent.GetComponentsInChildren<ToolBarSlot>(); //Get all the slots
       
       backpackUI = UIManager.instance.Backpack_UI;
        isToolBarEnabled = !backpackUI.IsUIShown;
        
    }

   
    void UpdateToolBar()
    {
        for (int i = 0; i < slots.Length; i++)
        { //Always loop all slots
            if (i < backpack.Toolbar.Length) //Check all the slots in the toolbar 
                                              
            {
                if (backpack.Toolbar[i] != null) //if an item is assigned 
                    slots[i].AddItem(backpack.Toolbar[i]); //Display the item to toolbar ui

                else
                    //The rest of the slots are empty
                    slots[i].ClearSlot();

            }
        }
    }
  
    private void Update()
    {
        isToolBarEnabled = !backpackUI.IsUIShown;
        if (isToolBarEnabled)
        {
            //Choose slot in order to be able to use item
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0)
            {
                if (selectIdx < 0)
                {
                    selectIdx = 0;
                    slots[selectIdx].ToggleHighlight();
                    return;
                }
                else
                    slots[selectIdx].ToggleHighlight();
                if (scroll < 0)
                    selectIdx = (selectIdx + 1) % backpack.ToolBarSize;
                else if (scroll > 0)
                    selectIdx = (backpack.ToolBarSize + selectIdx - 1) % backpack.ToolBarSize;

               
                slots[selectIdx].ToggleHighlight();

            }

            if (Input.GetButtonDown("E")) //If button E is pressed then unequiped active slot in toolbar
            {
                if(selectIdx > 0)
                    slots[selectIdx].ToggleHighlight();
                selectIdx = -1;
            }
        }



    }
}
