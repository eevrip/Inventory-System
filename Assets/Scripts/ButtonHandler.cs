using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button customButton;
    public BackpackSlot inventorySlot;
    public RightClickButton rightClickButton;
    void Awake()
    {
        customButton.onClick.AddListener(InventoryButton_onClick); //subscribe to the onClick event
        rightClickButton.onRightClick.AddListener(InventoryButton_onRightClick); //subscribe to the onClick event
    }
    
    //Handle the onClick event
    void InventoryButton_onClick()
    {
        inventorySlot.UseItem();
    }
    //Handle the onRightClick event
    void InventoryButton_onRightClick()
    {
        inventorySlot.DropItem();
    }
    void StorageButton_onClick()
    {
        inventorySlot.SwitchContainers();
    }
    public void EnableInventory()
    {
        Debug.Log("Inventory enable");
        customButton.onClick.AddListener(InventoryButton_onClick); //subscribe to the onClick event
        rightClickButton.onRightClick.AddListener(InventoryButton_onRightClick); //subscribe to the onClick event
        customButton.onClick.RemoveListener(StorageButton_onClick); //subscribe to the onClick event
    }
    public void EnableStorage()
    {
        Debug.Log("Storage enable");
        customButton.onClick.AddListener(StorageButton_onClick); //subscribe to the onClick event
        customButton.onClick.RemoveListener(InventoryButton_onClick); //subscribe to the onClick event
        rightClickButton.onRightClick.RemoveListener(InventoryButton_onRightClick); //subscribe to the onClick event
    }
}
