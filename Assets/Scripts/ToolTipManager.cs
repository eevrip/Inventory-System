using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;

public class ToolTipManager : MonoBehaviour
{
    #region Singleton
    public static ToolTipManager instance;
    private RectTransform rectangle;

    private RectTransform anchorSlot;
    private Animator anim;
    void Awake()
    {

        instance = this;
    }
    #endregion
    public TextMeshProUGUI description;
    public TextMeshProUGUI header;
    public TextMeshProUGUI food;
    public TextMeshProUGUI water;
    public TextMeshProUGUI health;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI defence;
    public GameObject bind;
    public GameObject use;
    public GameObject drop;
    public GameObject switchContainer;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        rectangle = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
    }


    public void SetToolTip(ItemObject item)
    {
        SetAllTextActive(true);
        if (item.type == ItemType.Consumable)
        {
            ConsumableObject consumableObject = (ConsumableObject)item;
            int food = consumableObject.food;
            int water = consumableObject.water;
            int health = consumableObject.health;

            if (food > 0)
                this.food.text = "Food +" + food;
            else if (food < 0)
                this.food.text = "Food " + food;
            else
                this.food.gameObject.SetActive(false);

            if (water > 0)
                this.water.text = "Water +" + water;
            else if (water < 0)
                this.water.text = "Water " + water;
            else
                this.water.gameObject.SetActive(false);

            if (health > 0)
                this.health.text = "Health +" + health;
            else if (health < 0)
                this.health.text = "Health +" + health;
            else
                this.health.gameObject.SetActive(false);

            this.defence.gameObject.SetActive(false);
            this.damage.gameObject.SetActive(false);
        }
        else if (item.type == ItemType.Equipment)
        {
            EquipmentObject equipmentObject = (EquipmentObject)item;
            int defence = equipmentObject.defence;

            if (defence != 0)
                this.defence.text = "Defence +" + defence;
            else
                this.defence.gameObject.SetActive(false);
            this.damage.gameObject.SetActive(false);
            this.food.gameObject.SetActive(false);
            this.water.gameObject.SetActive(false);
            this.health.gameObject.SetActive(false);
        }
        else if (item.type == ItemType.Tool)
        {

            ToolObject equipmentObject = (ToolObject)item;
            int damage = equipmentObject.damage;

            if (damage != 0)
                this.damage.text = "Damage +" + damage;
            else
                this.damage.gameObject.SetActive(false);
            this.defence.gameObject.SetActive(false);
            this.food.gameObject.SetActive(false);
            this.water.gameObject.SetActive(false);
            this.health.gameObject.SetActive(false);
        }
        else
        {
            SetAllTextActive(false);
            this.description.gameObject.SetActive(true);
            this.header.gameObject.SetActive(true);

        }
        this.header.text = item.title;
        this.description.text = item.description;

    }
    public void SetToolTipOnlyName(string header)
    {
       // this.header.alignment = TextAlignmentOptions.Center;
       SetAllTextActive(false);
       
        this.header.text = header; 
        //this.description.text = string.Empty; 
        this.header.gameObject.SetActive(true);
        
    }
    public void ShowToolTip(RectTransform rect)
    {
       
        anchorSlot = rect;
        //  this.header.text = header;
        //  this.description.text = description;


        float offsetX = 20f;

        Vector3 pos = anchorSlot.position; //Input.mousePosition;
        float pivotX = pos.x / Screen.width;
        float pivotY = pos.y / Screen.height;

        if (pivotX < 0.8f)
            pivotX = 0f;
        else
            pivotX = 1f;



        rectangle.pivot = new Vector2(pivotX, pivotY);




        if (pivotX == 0)
            transform.position = pos + new Vector3(offsetX, 0f, 0f);
        else
            transform.position = pos + new Vector3(-offsetX, 0f, 0f);
        gameObject.SetActive(true); 
        anim.SetTrigger("fadeIn");
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
        SetAllTextActive(true);
        anchorSlot = null;

        header.text = string.Empty;
        description.text = string.Empty;
    }
    public void SetAllTextActive(bool active)
    {
        this.description.gameObject.SetActive(active);
        this.header.gameObject.SetActive(active);
        this.damage.gameObject.SetActive(active);
        this.defence.gameObject.SetActive(active);
        this.food.gameObject.SetActive(active);
        this.water.gameObject.SetActive(active);
        this.health.gameObject.SetActive(active);
    }
    public void ActivateButtonsInventory()
    {
        bind.SetActive(true);
        use.SetActive(true);
        drop.SetActive(true);
        switchContainer.SetActive(false);
    }
    public void ActivateButtonsStorage()
    {
        bind.SetActive(false);
        use.SetActive(false);
        drop.SetActive(false);
        switchContainer.SetActive(true);
    }
    public void DeactivateButtonsInfo()
    {
        bind.SetActive(false);
        use.SetActive(false);
        drop.SetActive(false);
        switchContainer.SetActive(false);
    }
}
