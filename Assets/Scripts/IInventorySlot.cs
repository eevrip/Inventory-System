using UnityEngine;
using UnityEngine.UI;

public interface IInventorySlot
{
    public ItemObject Item { get; set; }
    public Image ImageIcon {set; }


    public int StaticIndex { get; }
    public void AddItem(ItemObject newItem);
    public void ClearSlot();


}
