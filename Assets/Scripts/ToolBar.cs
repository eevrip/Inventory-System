using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{

    #region Singleton
    //In order to have immediate access to the instance Inventory-There is only one inventory
    public static ToolBar instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ToolBar found!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnToolBarChanged();
    public OnToolBarChanged onToolBarChangedCallback;
   
    public static int spaceLimit = 5;



    //public List<ItemObject> container = new List<ItemObject>(spaceLimit); //Inventory's container

    public ItemObject[] container = new ItemObject[spaceLimit];
    void Start()
    {
       // Debug.Log("array size" + container.Count + container.Capacity);
    }

        //add item to a specific slot
        public void AssignItemToolBar(ItemObject item, int index)
    {

        // container.Add(item);
        container[index] = item;
        if (onToolBarChangedCallback != null)
            onToolBarChangedCallback.Invoke();
    }
    //add item in order
    public void Add(ItemObject item)
    {
        int k = EmptyPlace();
        if (k!=-1)
        {
       //if(!IsFull())
           //   container.Add(item);
            container[k] = item;
            if (onToolBarChangedCallback != null)
                onToolBarChangedCallback.Invoke();
        //}
        }
            
    }

    //removes iten from a specific place in toolbar
    public void Remove(int index)
    {

        // int k = container.FindIndex(x => (x.toolBarIndex == index));
        //Debug.Log("Toremove" + k);
        //  container.RemoveAt(k);
        ItemObject oldItem = container[index];
       // Inventory.instance.Remove(oldItem);
        container[index] = null;
        
        if (onToolBarChangedCallback != null)
            onToolBarChangedCallback.Invoke();


        //Spawn Item on the ground SpawnItem(item)
    }
    //This removes the first type of item found in the toolbar
    public void Remove(ItemObject item)
    {
         int k = ToolBarPlace(item);
        Debug.Log("k = " + item.prefab.name);
        if (k!= -1)
         {
       
           container[k] = null;
            Debug.Log("Item = " + item.prefab.name);
            

        }
        /*if (CheckInToolBar(item))
        {
            container.Remove(item);
            item.toolBarIndex = -1;
        }*/

        if (onToolBarChangedCallback != null)
            onToolBarChangedCallback.Invoke();


        //Spawn Item on the ground SpawnItem(item)
    }
    public bool CheckInToolBar(ItemObject item)
    {
       foreach (ItemObject i in container)
        {
            if (i == item)
                return true;
        }
        return false;
      /*  if(container.Contains(item))
            return true;
        else
          return false;*/
    }

    public bool IsFull()
    {
        foreach (ItemObject i in container)
        {
            if (i == null)
                return false;
        }
        return true;
        /*
        if (container.Count < spaceLimit)
            return false;
        else
            return true;*/
    }

    public int EmptyPlace()
    {
        for(int i=0;i<spaceLimit;i++)
        {
            if (container[i] == null)
                return i;
        }
        return -1;
       
    }
    public int ToolBarPlace(ItemObject item)
    {
       
        for(int i = spaceLimit-1;i>=0;i--)
        {
            if (container[i].ID == item.ID)
                return i;
        }
        return -1;
        
    }
}

