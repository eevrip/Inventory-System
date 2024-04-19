using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Storage Object", menuName = "Inventory System/Items/Storage")]
public class StorageObject : ItemObject
{

    public void Awake()
    {
        type = ItemType.Storage;
    }
}