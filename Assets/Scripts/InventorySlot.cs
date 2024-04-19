using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private ItemObject item;
    [SerializeField] private Image imageIcon;
    private int staticIndex;
    public ItemObject Item => item;
    public Image ImageIcon => imageIcon;
    public int StaticIndex => staticIndex;

    private bool isOnSlot;
    public bool IsOnSlot => isOnSlot;


    

    public virtual void Start()
    {
        staticIndex = transform.GetSiblingIndex();
        

    }
    public virtual void AddItem(ItemObject newItem)
    {
        item = newItem;
        imageIcon.sprite = item.icon;
        imageIcon.enabled = true;

        



    }

    public virtual void ClearSlot()
    {
        if (item != null)
        {
            item = null;
            imageIcon.sprite = null;
            imageIcon.enabled = false;
        }
      


    }
    public virtual void UpdateMode(bool isInventoryMode)
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isOnSlot = true;
    }
    public void OnPointerExit(PointerEventData eventData)
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
    }




    public virtual void AssignItemToolBar(int index)
    {
        

    }


    public virtual void Update()
    {
        if (isOnSlot)
            HoveringOver();
    }


}
