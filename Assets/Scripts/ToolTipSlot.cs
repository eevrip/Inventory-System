using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using static UnityEditor.Progress;

public class ToolTipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool showOnlyText;
    [SerializeField] private string textToShow;
    private ItemObject item;

    public ItemObject Item
    {
        get
        {
            return item;
        }
        set { item = value; }
    }

    private Coroutine delayToolTip;
    public Coroutine DelayToolTip => delayToolTip;
    public void OnPointerEnter(PointerEventData eventData)
    {
       
        if (showOnlyText) {
            if (textToShow != "")
            {
               
                ToolTipManager.instance.SetToolTipOnlyName(textToShow);
            }
            else if (item)
            {
               
                ToolTipManager.instance.SetToolTipOnlyName(item.title);
            }
        }
        else
        {
            ToolTipManager.instance.SetToolTip(item);
        }

        // delayToolTip = StartCoroutine(delayShow());
        ToolTipManager.instance.ShowToolTip(this.GetComponent<RectTransform>());

    }
    public void OnPointerExit(PointerEventData eventData)
    {
      //  if (delayToolTip != null)
       // {
      //      StopCoroutine(delayToolTip);
      //  }
        ToolTipManager.instance.HideToolTip();
    }




    IEnumerator delayShow()
    {

        yield return new WaitForSeconds(0f);
       
        ToolTipManager.instance.ShowToolTip(this.GetComponent<RectTransform>());
    }

}
