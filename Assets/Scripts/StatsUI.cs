using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private Slider currValue;

    [SerializeField] private StatsType type;
    [SerializeField] private TextMeshProUGUI text;
    private PlayerStateManager stateManager;
    private UIManager uiManager;
    private float maxVal;
    private int previousVal = 0;
    private Animator anim;
    void Start()
    {
        stateManager = PlayerStateManager.instance;
        uiManager = UIManager.instance;
        anim = GetComponent<Animator>();
        stateManager.consumeItem += SetSliderValue;

        uiManager.onOpenInventoryCall += ShowText;
        uiManager.onCloseInventoryCall += HideText;
        SetSliderMaxValue();
        SetSliderValue();
    }

    public void ShowText()
    {
        text.gameObject.SetActive(true);
    }
    public void HideText()
    {
        text.gameObject.SetActive(false);
    }
    public void SetSliderMaxValue()
    {
        switch (type)
        {
            case StatsType.Health:
                currValue.maxValue = stateManager.health.GetMaxValue();

                break;
            case StatsType.Water:
                currValue.maxValue = stateManager.water.GetMaxValue();
                break;
            case StatsType.Food:
                currValue.maxValue = stateManager.food.GetMaxValue();
                break;
            default:
                break;

        }
        maxVal = currValue.maxValue;
    }

    public void SetSliderValue()
    {

        switch (type)
        {
            case StatsType.Health:
                currValue.value = stateManager.health.GetCurrentVisualValue();

                break;
            case StatsType.Water:
                currValue.value = stateManager.water.GetCurrentVisualValue();

                break;
            case StatsType.Food:
                currValue.value = stateManager.food.GetCurrentVisualValue();

                break;
            default:
                break;

        }
        int intVal = Mathf.CeilToInt(currValue.value);
        if (previousVal != intVal)
        {

            text.text = intVal.ToString() + "/" + maxVal;
            previousVal = intVal;
        }
        if (intVal <= 30f)
        {
            anim.ResetTrigger("notDying");
            anim.SetTrigger("onDying");
        }
        else
        {
            anim.ResetTrigger("onDying");
            anim.SetTrigger("notDying");
        }

    }


    public enum StatsType
    {
        Health,
        Water,
        Food
    }
}
