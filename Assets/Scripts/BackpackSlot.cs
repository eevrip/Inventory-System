using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BackpackSlot : InventorySlot //, IPointerEnterHandler, IPointerExitHandler
{


    [SerializeField] private Image imageIcon2;
    public Image ImageIcon2 { set { imageIcon2 = value; } }
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject switchContainerButton;
    //  private bool isOnSlot;

    private StorageUI storageUI;

    public override void Start()
    {
        base.Start();
        storageUI = UIManager.instance.Storage_UI;

    }
    public override void AddItem(ItemObject newItem)
    {
        base.AddItem(newItem);

        imageIcon2.sprite = Item.icon;
        imageIcon2.enabled = true;



    }

    public override void ClearSlot()
    {
        base.ClearSlot();

        imageIcon2.sprite = null;
        imageIcon2.enabled = false;


    }

    public void DropItem()
    {
        //ToolBar.instance.Remove(item);//removes the first item found in toolbar
        //  Inventory.instance.RemoveSpawn(item);
        if (Item)
        {
            Item.Remove();
            PlayerInventory.instance.RemoveSpawn(StaticIndex);
        }
    }
    public void UseItem()
    {
        if (Item)
        {
            if (Item.type == ItemType.Consumable)
            {
                Item.Use();
                PlayerInventory.instance.Remove(StaticIndex);
            }
        }
        
    }
    public void SwitchContainers()
    {
        // Debug.Log("Switching containers");
        if (Item)
            if (storageUI.CurrentInventory.Add(Item))
                PlayerInventory.instance.Remove(StaticIndex);
    }


    public override void UpdateMode(bool isInventoryMode)
    {
        if (isInventoryMode)
        {
            inventoryButton.SetActive(true);
            switchContainerButton.SetActive(false);
            ToolTipManager.instance.ActivateButtonsInventory();
        }
        else
        {
            inventoryButton.SetActive(false);
            switchContainerButton.SetActive(true);
            ToolTipManager.instance.ActivateButtonsStorage();
        }
    }




    public override void AssignItemToolBar(int index)
    {

        if (Item)
        {
            Debug.Log(Item.prefab.name + "" + StaticIndex + "assign" + index);
            PlayerInventory.instance.AssignItemToolBarFromInventory(Item, index, StaticIndex);
            base.AssignItemToolBar(index);
        }

    }
    /*  public override void Update()
      {
          if (UIManager.instance.Backpack_UI.IsUIShown || UIManager.instance.Storage_UI.IsUIShown)
          {

              if (isOnSlot)
                  HoveringOver();
          }
          else
          {
              isOnSlot = false;
              Debug.Log("out " + StaticIndex);

          }
      }

      */

}
