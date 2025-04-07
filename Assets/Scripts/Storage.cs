using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Inventory, IInteractable
{
    [SerializeField] List<ItemObject> itemsInStorage = new List<ItemObject>();
    public List<ItemObject> ItemInStorage => itemsInStorage;
    [SerializeField]
    private ItemObject item;
    public ItemObject Item { get { return item; } set { item = value; } }
    [SerializeField] private string message;
    public string Message
    {
        get { return message; }
        set
        {
            message = value;
        }
    }
    //  public delegate void OnStorageUpdate();
    //  public OnStorageUpdate onStorageUpdateCallback;
    // public delegate void OnItemUpdate();
    //  public OnItemUpdate onItemUpdateCallback;
    private StorageUI storageUI;
   

    void Start()
    {
     
        storageUI = UIManager.instance.Storage_UI;
        message = "Open storage";
        for(int i=0; i< itemsInStorage.Count; i++)
        {
            Add(itemsInStorage[i]);
        }
    }

    public void Interact()
    {
        

        if (storageUI != null)
        {
            storageUI.SetStorage(this);
            storageUI.ShowInventory();
        }
        
    }



    public override bool Add(ItemObject item)
    {
        

        if (!InventoryContainer.AddToInventory(item))
            return false;
        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();
        return true;
    }

    public override void Remove(ItemObject item)
    {
       
            InventoryContainer.RemoveFromInventory(item);
       
       

        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();



    }


    
}
