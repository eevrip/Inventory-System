using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField]
    public string statName;
    [SerializeField]
    private float baseValue;
    [SerializeField]
    private float reducingRate;
    [SerializeField] private float maxValue = 100f;
    private float currentValue;
    private float currentVisualValue;
    private float addOnValue;
    private float targetValue;
    public float TargetValue
    {
        get { return targetValue; } 
        set
        {
            if (value > maxValue)
            {
                targetValue = maxValue;
               
            }
            else if (value < 0f)
                targetValue = 0f;
            else
                targetValue = value;
        }
    }
         
    private bool isUpdating = false;
    public bool IsUpdating
    {
        get { return isUpdating; }
        set { isUpdating = value; }
    }
    private Coroutine statUpdating;
    public Coroutine StatUpdating
    {
        get { return statUpdating; }
        set { statUpdating = value; }
    }
    public Stats()
    {
        ResetValue();
    }
    public float GetMaxValue() { return maxValue; }
    public float GetReducingRate() { return reducingRate; }
    public string GetName() { return statName; }
    public float GetValue()
    {
        float val = baseValue + addOnValue;
        if (val < maxValue)
        {
            return val;
        }
        return maxValue;
    }
   
    public void AddValue(float addOn)
    {
        addOnValue = addOn;
        // currentValue += addOnValue;

    }



    public float GetCurrentValue()
    {


        return currentValue;

    }
    public float GetCurrentVisualValue()
    {


        return currentVisualValue;

    }
    public void SetCurrentValue(float value)
    {
        if (value > maxValue)

            currentValue = maxValue;
        else if (value < 0f)
            currentValue = 0f;
        currentValue = value;
    }
    public void SetCurrentVisualValue(float value)
    {
        if (value > maxValue)

            currentVisualValue = maxValue;
        else if (value < 0f)
            currentVisualValue = 0f;
        currentVisualValue = value;
    }
    public float UpdateCurrentVisualValue(float addOn)
    {
        addOnValue = addOn;
        currentVisualValue = currentVisualValue + addOn;
        if (currentVisualValue < maxValue && currentVisualValue > 0f)
        {
            return currentVisualValue;
        }
        else if (currentVisualValue < 0f)
            return 0f;
        return maxValue;
    }
    public float UpdateCurrentValue(float addOn)
    {
        addOnValue = addOn;
        currentValue = currentValue + addOn;
        if (currentValue < maxValue && currentValue > 0f)
        {
            return currentValue;
        }
        else if (currentValue < 0f)
            return 0f;
        return maxValue;
    }
    public void ResetValue()
    {
        currentValue = baseValue;
        currentVisualValue = currentValue;
        targetValue = currentValue;
        addOnValue = 0f;
    }
}
