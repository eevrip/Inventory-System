using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolBarSlot : InventorySlot//, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField] private GameObject slotHighlight;
  
    private bool isSelected = false;
    private ToolBarUI toolbarUI;

    [SerializeField]
    private PlayerAnimation anim;
    public override void Start()
    {
        base.Start();
        toolbarUI = transform.parent.parent.GetComponent<ToolBarUI>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAnimation>();
    }

    public override void ClearSlot()
    {
       
        if (isSelected)
        {
            UnequipSlot();
        }
        base.ClearSlot();
    }
    public override void AddItem(ItemObject newItem)
    {
        base.AddItem(newItem);
        if (isSelected)
        {
            EquipSlot();
        }
    }
    public void SelectSlot()
    {   //Left click
       
      //  if (Input.GetMouseButtonDown(0))
      //  {
      //      UseItem();
      //  }
       // else
       //right click
       if (Input.GetMouseButtonDown(1))
        {
            if (isSelected)
            {
                UnequipSlot();
            }
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
            {// ToolManager.instance.SpawnTool(true, Item);

                toolbarUI.SetControlInfo(0);
                toolbarUI.SetControlInfo(1);

                Debug.Log("Equip");
                Item.EquipItem();
                anim.SetLeftHandPickingUp();

            }
            else if (Item.type == ItemType.Consumable)
            {
                toolbarUI.SetControlInfo(0);
                toolbarUI.SetControlInfo(1);
            }
            else
            { toolbarUI.SetControlInfo(1); toolbarUI.DeactivateControl(0); }
        }
    }
    public void UnequipSlot()
    {
        toolbarUI.DeactivateControl(0);
        toolbarUI.DeactivateControl(1);
        if (Item != null)
        {
           
            if (Item.type == ItemType.Tool)
            { //ToolManager.instance.SpawnTool(false, Item);
                Debug.Log("Unequip");
                Item.UnequipItem();
                anim.SetRightHandPickingUp();
            }
        }
    }
   

    public void DropItem()
    {
      //Remove and spawn item given a specific position at toolbar
      Item.Remove();
        PlayerInventory.instance.RemoveSpawnToolBar(StaticIndex);
        


    }
   /* public void UseItem()
    {
        
        if (Item != null)
        {//This doesn't allow to use goods or valuables when in toolbar - meaning that the player cannot consume(goods) or expend to craft(valuable)
            if(Item.type == ItemType.Tool)
                Item.Use();
        }

    }*/


    public override void AssignItemToolBar(int index)
    {

        if(index != StaticIndex)
        {
            PlayerInventory.instance.AssignItemToolBarFromBar(Item, index, StaticIndex);
            base.AssignItemToolBar(index);
        }
 
    }
   public override void Update()
    {
        if (toolbarUI.IsToolBarEnabled)
        {
            if (isSelected && Item != null)
                SelectSlot();
        }
       
        base.Update();
    }
}
