using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolBarSlot : InventorySlot
{
    
    [SerializeField] private GameObject slotHighlight;
  
    private bool isSelected = false;
    private ToolBarUI toolbarUI;
   

    public override void Start()
    {
        base.Start();
        toolbarUI = transform.parent.parent.GetComponent<ToolBarUI>();
    }
   
    public void SelectSlot()
    {   //Left click
       
        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DropItem();
        }
    }
    public void ToggleHighlight()
    {//Already active. Hence this unequips the item
        if (slotHighlight.activeSelf)
            UnequipSlot();
        else
            EquipSlot();

        slotHighlight.SetActive(!slotHighlight.activeSelf);
        if (slotHighlight.activeSelf)
            isSelected = true;
        else
            isSelected = false;
    }
    public void EquipSlot()
    {
        if (Item != null)
        {
            if (Item.type == ItemType.Tool)
            {
                Debug.Log("Equip");
                Item.EquipItem();
            }
        }
    }
    public void UnequipSlot()
    {
        if (Item != null)
        {
            if (Item.type == ItemType.Tool)
            {
                Debug.Log("Unequip");
                Item.UnequipItem();
            }
        }
    }
   

    public void DropItem()
    {
      //Remove and spawn item given a specific position at toolbar
        PlayerInventory.instance.RemoveSpawnToolBar(StaticIndex);
       
       
    }
    public void UseItem()
    {
        
        if (Item != null)
        {//This doesn't allow to use goods or valuables when in toolbar - meaning that the player cannot consume(goods) or expend to craft(valuable)
            if(Item.type == ItemType.Tool)
                Item.Use();
        }

    }
   
    

    public override void AssignItemToolBar(int index)
    {

        if(index != StaticIndex)
        {
            PlayerInventory.instance.AssignItemToolBarFromBar(Item, index, StaticIndex);
            
        }
 
    }
   public override void Update()
    {
        if (toolbarUI.isToolBarEnabled)
        {
            if (isSelected && Item != null)
                SelectSlot();
        }
        // if (isOnSlot)
        //   HoveringOver();
        base.Update();
    }
}
