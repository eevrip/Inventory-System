using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    #region Singleton
    public static PlayerStateManager instance;


    void Awake()
    {
        instance = this;
    }
    #endregion


    public Stats health;
    public Stats water;
    public Stats food;
    public Stats damage;
    public Stats defence;
    ToolManager toolManager;
    EquipmentManager equipmentManager;
    [SerializeField] private float timeCountdown;

    public delegate void OnConsumeItem();
    public OnConsumeItem consumeItem;


    // Stats defense;
    void Start()
    {

        health.ResetValue();
        water.ResetValue();
        food.ResetValue();
        defence.ResetValue();
        damage.ResetValue();

        toolManager = ToolManager.instance;
        toolManager.onToolUpdateCallback += onToolUpdate;
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentUpdateCallback += onEquipmentUpdate;
        Debug.Log("manager health = " + health.GetCurrentValue() + "base " + health.GetValue());
        water.StatUpdating = StartCoroutine(ReduceFullnessOverTime(water));

        food.StatUpdating = StartCoroutine(ReduceFullnessOverTime(food));
        health.StatUpdating = null;
    }

    public void onToolUpdate(ToolObject item, bool addingState)
    {

        damage.AddValue(toolManager.currToolStats[0]);
        Debug.Log(item.title + " " + damage.GetValue());


    }
    public void onEquipmentUpdate(EquipmentObject item)
    {

        defence.AddValue(equipmentManager.currEquipStats[0]);


    }
    public void onConsumableUpdate(ConsumableObject newConsumable)
    {


        water.TargetValue = water.TargetValue + newConsumable.water;
         food.TargetValue = food.TargetValue + newConsumable.food;
         health.TargetValue = health.TargetValue + newConsumable.health;
      
        StartCoroutine(IncreaseFullnessOverTime(water, water.TargetValue, newConsumable.water));

        StartCoroutine(IncreaseFullnessOverTime(food, food.TargetValue,newConsumable.food));
        StartCoroutine(IncreaseFullnessOverTime(health, health.TargetValue, newConsumable.health));
         

    
}

    public void TakeDamage(float damageAmount)
    {
        if (health.GetCurrentVisualValue() > 0)
        {
            health.UpdateCurrentVisualValue(-damageAmount);
            
        }
        else
        {
            //Die();
        }
    }

    private void CriticalLevels(Stats stat)
    {

        string msg = stat.GetName() + " levels critical.";
        PopUpMessagesManager.instance.ShowPopUpMessage(msg);
        if(health.StatUpdating == null) { 
        health.StatUpdating = StartCoroutine(ReduceFullnessOverTime(health));
        }
    }
    private void LowLevels(Stats stat)
    {
        string msg = stat.GetName() + " imminent.";
        PopUpMessagesManager.instance.ShowPopUpMessage(msg);
    }
    private IEnumerator ReduceFullnessOverTime(Stats stat)
    {
        float maxValue = stat.GetMaxValue();
        float rate = stat.GetReducingRate();
        // This will keep reducing fullness until it reaches 0
      
        while (stat.GetCurrentVisualValue() > 30f && !stat.IsUpdating)
        {
            
            stat.UpdateCurrentVisualValue(-rate * Time.deltaTime);
         
            stat.TargetValue = stat.GetCurrentVisualValue();
            if (consumeItem != null)
                consumeItem.Invoke();
            yield return null;  // Wait for the next frame before continuing
        }
        if (stat!= health)
            LowLevels(stat);
        while (stat.GetCurrentVisualValue() > 15f && !stat.IsUpdating)
        {

            stat.UpdateCurrentVisualValue(-rate * Time.deltaTime);
         
            stat.TargetValue = stat.GetCurrentVisualValue();
            if (consumeItem != null)
                consumeItem.Invoke();
            yield return null;  // Wait for the next frame before continuing
        }
        if (stat != health)
          CriticalLevels(stat);
        while (stat.GetCurrentVisualValue() > 0f && !stat.IsUpdating)
        {

            stat.UpdateCurrentVisualValue(-rate * Time.deltaTime);

            stat.TargetValue = stat.GetCurrentVisualValue();
            if (consumeItem != null)
                consumeItem.Invoke();
            yield return null;  // Wait for the next frame before continuing
        }
        if (stat == health)
        {
            Debug.Log("Die");
        }

    }
    private IEnumerator IncreaseFullnessOverTime(Stats stat, float targetVal, float addOn)
    {
        if (addOn ==  0)
            yield break;
        stat.IsUpdating = true;
        //Stop coroutine for redusing value over time - for food and water
        if (stat.StatUpdating != null && stat != health)
        {
           // Debug.Log("Stop cor " + stat.statName);
            StopCoroutine(stat.StatUpdating);
            stat.StatUpdating = null;   
        }

        float startVal = stat.GetCurrentVisualValue();
      
       
      
        float rate = 5f;
        float step = 0f;
        
        while (stat.GetCurrentVisualValue() < stat.TargetValue)
        {

            if(targetVal != stat.TargetValue)
            {
                
                yield break;
            }
            stat.SetCurrentVisualValue(Mathf.Lerp(startVal, stat.TargetValue, step));
         
            step += rate * Time.deltaTime;
            
            
            if (consumeItem != null)
                consumeItem.Invoke();

            yield return null;  // Wait for the next frame before continuing
        }
        if (stat.GetCurrentVisualValue() > 30f && stat != health && health.StatUpdating != null)
        {
           // Debug.Log("Stop coroutine health damage");
            StopCoroutine(health.StatUpdating);
            health.StatUpdating = null;
        }
        stat.IsUpdating = false;
        if (stat != health)
        {
           
            stat.StatUpdating = StartCoroutine(ReduceFullnessOverTime(stat));

        }

        yield break;

    }
    private IEnumerator ReduceFullness(Stats stat, float addOn)
    {
        float currentVal = stat.GetCurrentValue();

        float targetVal = currentVal - addOn;
        // stat.UpdateCurrentValue(-addOn);
        stat.SetCurrentValue(targetVal);
        float rate = 3f;
        float step = 0f;
        // This will keep reducing fullness until it reaches 0
        while (stat.GetCurrentVisualValue() > targetVal)
        {


            stat.SetCurrentVisualValue(Mathf.Lerp(stat.GetCurrentValue(), targetVal, step));
            step -= rate * Time.deltaTime;
            if (consumeItem != null)
                consumeItem.Invoke();
            yield return null;  // Wait for the next frame before continuing
        }

    }
}
