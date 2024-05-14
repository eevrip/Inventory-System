using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{

    //In order to have immediate access to the instance Inventory-There is only one inventory
    public static PlayerInventory instance;

    protected override void Awake() {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;

        toolbar = new ItemObject[toolBarSize];
        pairing = new int[toolBarSize];

    }


   // public delegate void OnItemUpdate();
  //  public OnItemUpdate onItemUpdateCallback;

    private GameObject player;

    [SerializeField] private int toolBarSize = 5;
    public int ToolBarSize { get { return toolBarSize; } }
    [SerializeField] private ItemObject[] toolbar;
    public ItemObject[] Toolbar => toolbar;
    [SerializeField]private Transform environment;
    private int[] pairing; //= new int[toolBarSize];

    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < pairing.Length; i++)
        {
            pairing[i] = -1;
        }
    }

    //Add items in both backpack and toolbar if there is space
    public override bool Add(ItemObject item) {
     
       if(!InventoryContainer.AddToInventory(item))
            return false;
       

        AddToolBar(item);

        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();
        return true;
    }
   
    //Removes item from inventory and toolbar and spawn it to the world given the inventory slot index
    public void RemoveSpawn(int index)
    {

        ItemObject item = InventoryContainer.Inventory[index];
        InventoryContainer.RemoveFromInventory(index);
        int toolBarSlot = GetToolBarIndex(index);
        if (toolBarSlot != -1)
            RemoveToolBarOnly(toolBarSlot);
        AdjustPairing(false, index);
        //Spawn Item on the ground 
        SpawnItem(item);

        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();



    }
    //Removes item from inventory and toolbar given the inventory slot index
    public void Remove(int index)
    {

       // ItemObject item = InventoryContainer.Inventory[index];
        InventoryContainer.RemoveFromInventory(index);
        int toolBarSlot = GetToolBarIndex(index);
        if (toolBarSlot != -1)
            RemoveToolBarOnly(toolBarSlot);
        AdjustPairing(false, index);
       
        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();



    }
    //Spawns item in the world
    public void SpawnItem(ItemObject item) {

        Vector3 newPos = player.transform.position + player.transform.forward * 2f + new Vector3(0f, 1f, 0f);// + add a height ?
        GameObject temp;
        temp = Instantiate(item.prefab, newPos, Quaternion.identity, environment);  //
        temp.GetComponent<Rigidbody>().isKinematic = false;

    }




    //Assigns an item at a specified position in toolbar. It already exists in the backpack

    public void AssignItemToolBarFromInventory(ItemObject item, int newSlot, int inventorySlot)
    {
        int oldSlot = -1;
        oldSlot = GetToolBarIndex(inventorySlot);
        int pair = -1;
        if (oldSlot != -1)
        {
            toolbar[oldSlot] = null;
            pair = pairing[oldSlot];
            pairing[oldSlot] = -1;
        }
        if (pair == -1)
            pair = inventorySlot;

        toolbar[newSlot] = item;
        pairing[newSlot] = pair;

        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();
    }
    //Assigns an item at a specified position that already exists in toolbar slot to a different one. It already exists in the backpack
    public void AssignItemToolBarFromBar(ItemObject item, int newSlot, int oldSlot)
    {
        int pair = -1;
        if (oldSlot != -1)
        {
            toolbar[oldSlot] = null;
            pair = pairing[oldSlot];
            pairing[oldSlot] = -1;
        }


        toolbar[newSlot] = item;
        pairing[newSlot] = pair;

        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();
    }

    //Adds item to toolbar at the first empty space found
    public void AddToolBar(ItemObject item)
    {
        int lastPlace = InventoryContainer.Inventory.FindLastIndex(x => x.ID == item.ID);
        int k = EmptyPlaceToolBar();


        AdjustPairing(true, lastPlace);
        if (k != -1)//toolbar is not full
        {
            toolbar[k] = item;
            pairing[k] = lastPlace;

        }


    }

    //Removes item from a given specific position in toolbar. It also removes the item from the backpack
    public void RemoveToolBar(int index)
    {


       
        int inventorySlot = pairing[index];
        InventoryContainer.RemoveFromInventory(inventorySlot);
        toolbar[index] = null;
        pairing[index] = -1;
        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();


    }

    //Removes and Spawns item from a given specific position in toolbar. It also removes the item from the backpack. Only called from toolbar slot
    public void RemoveSpawnToolBar(int index)
    {

        ItemObject oldItem = toolbar[index];
       
        int inventorySlot = pairing[index];
        InventoryContainer.RemoveFromInventory(inventorySlot);
        toolbar[index] = null;
        pairing[index] = -1;
        AdjustPairing(false, inventorySlot);
        SpawnItem(oldItem);

        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();


    }
    //removes item given the toolbar slot index, only from toolbar -  not backpack
    public void RemoveToolBarOnly(int index)
    {


        toolbar[index] = null;
        pairing[index] = -1;
        if (onItemUpdateCallback != null)
            onItemUpdateCallback.Invoke();


    }
   
    //Adjust the pairing of toolbar slots and inventory slots when adding/removing items given the inventory slot index
    public void AdjustPairing(bool add, int index)
    {
        if (add)
        {
            for (int i = toolBarSize - 1; i >= 0; i--)
            {
                if (pairing[i] >= index)
                    pairing[i] = pairing[i] + 1;
            }
            
        }
        else
        {
            for (int i = toolBarSize - 1; i >= 0; i--)
            {
                if (pairing[i] > index)
                    pairing[i] = pairing[i] - 1;
            }
        }

    }
  

    public int EmptyPlaceToolBar()
    {
        for (int i = 0; i < toolBarSize; i++)
        {
            if (toolbar[i] == null)
                return i;
        }
        return -1;

    }
    //Return the toolbar slot index given the inventory slot
    public int GetToolBarIndex(int inventorySlot)
    {
        for (int i = 0; i < pairing.Length; i++)
        {
            if (inventorySlot == pairing[i])
            {
                return i;
            }
        }
        return -1;
    }
   
}
