using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeftClickButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onLeftClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Invoke the left click event
            onLeftClick.Invoke();
        }
    }
}
