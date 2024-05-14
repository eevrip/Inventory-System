using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField]
    private int baseValue;
    private int currentValue;
    private int addOnValue;

    public Stats()
    {
        ResetValue();
    }
    public int GetValue()
    {
       //currentValue = baseValue + addOnValue;
        return baseValue + addOnValue;
    }
    public void AddValue(int addOn)
    {
        addOnValue = addOn;
       // currentValue += addOnValue;

    }
    
    
    public void RemoveValue(int removeVal) {
    
        addOnValue = -removeVal;
    }
    public int GetCurrentValue()
    { 
        currentValue = currentValue + addOnValue;
        return currentValue;

    }
    public void ResetValue() {
        currentValue = baseValue;
        addOnValue = 0;
    }
}
