using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarUI : MonoBehaviour
{
    [SerializeField] private GameObject toolBarSlotsParent;
    ToolBar toolBar;
    ToolBarSlot[] slots;
    PlayerInventory inventory;
    public BackpackUI inventoryUI;
    public int selectIdx = -1;
    public bool isToolBarEnabled;
    // Start is called before the first frame update
    void Start()
    {
        // toolBar = ToolBar.instance;
        // toolBar.onToolBarChangedCallback += UpdateToolBar; //update UI depending on the ToolBar changes
        inventory = PlayerInventory.instance;
        inventory.onItemUpdateCallback += UpdateToolBar; //update UI depending on the Inventory changes

        slots = toolBarSlotsParent.GetComponentsInChildren<ToolBarSlot>(); //Get all the slots
                                                                           //  slots[selectIdx].ToggleHighlight();

        isToolBarEnabled = !inventoryUI.IsUIShown;
    }

   
    void UpdateToolBar()
    {
        for (int i = 0; i < slots.Length; i++)
        { //Always loop all slots
            if (i < inventory.Toolbar.Length) //Check all the slots in the toolbar 
                                              //if(i<toolBar.container.Count)
            {
                if (inventory.Toolbar[i] != null) //if an item is assigned 
                    slots[i].AddItem(inventory.Toolbar[i]); //Display the item to toolbar ui

                else
                    //The rest of the slots are empty
                    slots[i].ClearSlot();

            }
        }
    }
  
    private void Update()
    {
        isToolBarEnabled = !inventoryUI.IsUIShown;
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
                    selectIdx = (selectIdx + 1) % inventory.ToolBarSize;
                else if (scroll > 0)
                    selectIdx = (inventory.ToolBarSize + selectIdx - 1) % inventory.ToolBarSize;

                //  Debug.Log(selectIdx);
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
