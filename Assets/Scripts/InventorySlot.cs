using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private ItemObject item;
    [SerializeField] private Image imageIcon;
    private int staticIndex;
    public ItemObject Item => item;
    public Image ImageIcon => imageIcon;
    public int StaticIndex => staticIndex;

    private Coroutine delayToolTip;
    public Coroutine DelayToolTip => delayToolTip;

    private bool isOnSlot;
    public bool IsOnSlot
    {
        get { return isOnSlot; }
        set { isOnSlot = value; }
    }

    public delegate void OnSelectItem(ItemObject obj);
    public OnSelectItem callOnSelectItem;

    public virtual void Start()
    {
        staticIndex = transform.GetSiblingIndex();
        

    }
    public virtual void AddItem(ItemObject newItem)
    {
        item = newItem;
        imageIcon.sprite = item.icon;
        imageIcon.enabled = true;
      

      //Update tooltip details if the item is updated
        if(item && isOnSlot)
        {
            ToolTipManager.instance.SetToolTip(item);
        }

    }
    
   
    public virtual void ClearSlot()
    {
        if (item != null)
        {
            item = null;
            imageIcon.sprite = null;
            imageIcon.enabled = false;
            
        }

       // if (delayToolTip != null)
        //{
       //     StopCoroutine(delayToolTip);
       // }

    }
    public virtual void UpdateMode(bool isInventoryMode)
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isOnSlot = true;
        //Show description of item
        if (item)
        {
            ToolTipManager.instance.SetToolTip(item);
            ToolTipManager.instance.ShowToolTip(this.GetComponent<RectTransform>());
            //delayToolTip = StartCoroutine(delayShow());

        }
       // Debug.Log("in " + StaticIndex);
    }
    public void OnPointerExit(PointerEventData eventData)
    {

        isOnSlot = false;
        //if (delayToolTip != null)
       // {
       //     StopCoroutine(delayToolTip);
      //  }
        ToolTipManager.instance.HideToolTip();
       // Debug.Log("out " + StaticIndex);
    }

   
    public void OnDisable()
    {
        isOnSlot = false;

    }

    public void HoveringOver()
    {
        int index = -1;
        if (Input.GetButtonDown("Keypad1"))
        {

            index = 0;
        }
        else if (Input.GetButtonDown("Keypad2"))
        {

            index = 1;
        }
        else if (Input.GetButtonDown("Keypad3"))
        {

            index = 2;
        }
        else if (Input.GetButtonDown("Keypad4"))
        {
            index = 3;
        }
        else if (Input.GetButtonDown("Keypad5"))
        {
            index = 4;
        }

        if (index != -1)
            AssignItemToolBar(index);
        if (!item)
        {
            ToolTipManager.instance.HideToolTip();
        }
    }



     public virtual void AssignItemToolBar(int index)
     {
        if(callOnSelectItem != null)
            callOnSelectItem.Invoke(item);
     }


      public virtual void Update()
       {
          if (isOnSlot)
              HoveringOver();
       }
    IEnumerator delayShow()
    {

        yield return new WaitForSeconds(1f);
        if(item)
            ToolTipManager.instance.ShowToolTip(this.GetComponent<RectTransform>());
    }

}
