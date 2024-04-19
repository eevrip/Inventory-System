using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Inventory, IInteractable
{
   
    [SerializeField]
    private ItemObject item;
    public ItemObject Item { get { return item; } set { item = value; } }

  //  public delegate void OnStorageUpdate();
  //  public OnStorageUpdate onStorageUpdateCallback;
   // public delegate void OnItemUpdate();
  //  public OnItemUpdate onItemUpdateCallback;
    private StorageUI storageUI;
   

    void Start()
    {
     
        storageUI = UIManager.instance.Storage_UI;
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
        /* if(chestContainer.Count >= chestSize)
         {
             Debug.Log("Chest is full");
             return false;
         }
         chestContainer.Add(item);

         chestContainer.Sort(ComparingID);*/

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
