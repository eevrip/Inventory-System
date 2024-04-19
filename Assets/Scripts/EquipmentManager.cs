using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    EquipmentObject[] currEquipment; //current equipment

    PlayerInventory inventory;
    public delegate void equipmentUpdate(EquipmentObject item);
    public equipmentUpdate onEquipmentUpdateCallback;
    public int[] currEquipStats = new int[2];

    void Start()
    {
        inventory = PlayerInventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentKind)).Length;
        currEquipment = new EquipmentObject[numSlots]; //create array with the same dimensions as the number of the kinds of equipment

    }

    public void AddStats(EquipmentObject obj)
    {
        currEquipStats[0] = obj.defence;
       // currEquipStats[1] = obj.defence;
        if (onEquipmentUpdateCallback != null)
            onEquipmentUpdateCallback.Invoke(obj);
    }

    public void RemoveStats(EquipmentObject obj)
    {
        currEquipStats[0] = 0;
       // currEquipStats[1] = 0;
        if (onEquipmentUpdateCallback != null)
            onEquipmentUpdateCallback.Invoke(obj);
    }
    
    public void Equip(EquipmentObject newItem) {
        int kindIndex = (int)newItem.equipmentKind;

        EquipmentObject oldItem = null;

        if (currEquipment[kindIndex] != null) { //Swap items that are the same kind 
            oldItem = currEquipment[kindIndex];
            inventory.Add(oldItem); //Add it to the inventory again
        }
        currEquipment[kindIndex] = newItem; //Find the kind of the equipment and place it to the right place
    }
    
}
