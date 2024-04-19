using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//Add this to add a right click function on a button
public class RightClickButton: MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onRightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Invoke the right click event
            onRightClick.Invoke();
        }
    }
}
