using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using static UnityEditor.Progress;

public class ToolTipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemObject item;

    public ItemObject Item { get { return item;
}
        set { item = value; }}

    private Coroutine delayToolTip;
    public Coroutine DelayToolTip => delayToolTip;
    public void OnPointerEnter(PointerEventData eventData)
    { 
        ToolTipManager.instance.SetToolTipOnlyName(item.title);
        delayToolTip = StartCoroutine(delayShow());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (delayToolTip != null)
        {
            StopCoroutine(delayToolTip);
        }
        ToolTipManager.instance.HideToolTip();
    }




    IEnumerator delayShow()
    {

        yield return new WaitForSeconds(1f);
        if (item)
            ToolTipManager.instance.ShowToolTip(this.GetComponent<RectTransform>());
    }

}
