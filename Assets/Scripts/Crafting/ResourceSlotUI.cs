using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResourceSlotUI : ToolTipSlot
{
   
    [SerializeField] private Image img;
    [SerializeField] private Image hasItem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void UpdateDetails(ItemObject resource)
    {
        
        Item = resource;
        img.sprite = resource.icon;

        
    }
    public void HasItemInBackpack(bool hasItemInPocket)
    {
        hasItem.enabled = !hasItemInPocket;
    }
}
