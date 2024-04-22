using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] private BackpackUI backpackUI;
    [SerializeField] private StorageUI storageUI;

      public BackpackUI Backpack_UI => backpackUI;
      public StorageUI Storage_UI => storageUI;

    private bool isStorageOpen = false;
    public bool IsStorageOpen { get { return isStorageOpen; } }
    private bool wasStorageClosed = false; //was storage closed before

    private bool isUIEnabled = false;
    public bool IsUIEnabled { get { return isUIEnabled; } }


    void Update()
    {
        isStorageOpen = storageUI.IsUIShown;
        if (!isStorageOpen) //Storage is not enabled
        {
            if (Input.GetButtonDown("Tab")) //If button tab is pressed then the UI for the inventory appears
            {
                if (wasStorageClosed)
                {
                    backpackUI.InventoryMode(true);
                    wasStorageClosed = false;
                }
                if (!backpackUI.IsUIShown)
                {
                    isUIEnabled = true;
                    backpackUI.ShowInventory();
                }
                else
                {   isUIEnabled = false;
                    backpackUI.CloseInventory();
                }
            }
        }
        else //Storage is enabled
        {
            isUIEnabled = true;
            if (Input.GetButtonDown("Esc"))
            {
                isUIEnabled = false;
                storageUI.CloseInventory();
                backpackUI.CloseInventory();
                wasStorageClosed = true;



            }
        }

    }
}
