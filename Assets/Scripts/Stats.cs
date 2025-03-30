using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField]
    private string statName;
    [SerializeField]
    private float baseValue;
    [SerializeField]
    private float reducingRate;
    [SerializeField] private float maxValue = 100f;
    private float currentValue;
    private float currentVisualValue;
    private float addOnValue;

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
        if(value > maxValue)

            currentValue = maxValue;
        else if(value <0f)
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
    public void ResetValue() {
        currentValue = baseValue;
        currentVisualValue = currentValue;
        addOnValue = 0f;
    }
}
