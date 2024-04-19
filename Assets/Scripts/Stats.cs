using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField]
    private int baseValue;

    private int currentValue;

    public Stats()
    {
        ResetValue();
    }
    public int GetValue()
    {
       
        return baseValue + currentValue;
    }
    public void SetValue(int addOn)
    {
        currentValue = addOn;

    }
    
    /*public void SetValue(int newValue)
    {
        currentValue = newValue;
        if(newValue< baseValue)
        {
            ResetValue();
        }
            
        
    }*/
    public void ResetValue()
    {
        currentValue = baseValue;
    }
}
