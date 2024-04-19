using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    [SerializeField] protected InventorySystem inventoryContainer;
    public InventorySystem InventoryContainer => inventoryContainer;

    protected virtual void Awake()
    {
        inventoryContainer = new InventorySystem(inventorySize);
    }

    public virtual bool Add(ItemObject item)
    {
        Debug.Log("Nothing happening");
        return false;
    }
    public delegate void OnItemUpdate();
    public OnItemUpdate onItemUpdateCallback;
}

