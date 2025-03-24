using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResourceSlotUI : ToolTipSlot
{
   
    [SerializeField] private Image img;
    [SerializeField] private Image hasItem;
    [SerializeField] private TextMeshProUGUI amountRequiredText;
   [SerializeField]private Color32 colorActive = new Color32(138, 244, 255, 255);
    [SerializeField] private Color32 colorDeactive = new Color32(9, 12, 13, 255);
    private int currAmount;
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
    public void UpdateDetails(Ingredient resource)
    {

        Item = resource.item;
        
        img.sprite = resource.item.icon;
       
        currAmount = PlayerInventory.instance.AmountOf(resource.item); 
        amountRequiredText.text = resource.item.title + " " +  resource.amount.ToString() + " (" + currAmount.ToString() + ")";
        HasItemInBackpack(Ingredient.IsLarger(currAmount, resource.amount));


        
    }
    public void HasItemInBackpack(bool hasItemInPocket)
    {
        hasItem.enabled = !hasItemInPocket;
        if (hasItemInPocket)
        {
            amountRequiredText.color = colorActive;
        }
        else
            amountRequiredText.color = colorDeactive;
    }
}
