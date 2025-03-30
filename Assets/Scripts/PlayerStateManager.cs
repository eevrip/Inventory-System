using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    #region Singleton
    public static PlayerStateManager instance;

    public delegate void OnConsumeItem(bool animTriggered);
    public OnConsumeItem consumeItem;
    void Awake()
    {
        instance = this;
    }
    #endregion
    
    int currHealth;
    public Stats health;
    public Stats water;
    public Stats food;
    public Stats damage;
    public Stats defence;
    ToolManager toolManager;
    EquipmentManager equipmentManager;
    [SerializeField] private float timeCountdown;


    public bool healthZero;
    public bool waterZero;
    public bool foodZero;
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
        Debug.Log("manager health = " + health.GetCurrentValue() + "base " + health.GetValue() );
        StartCoroutine(ReduceFullnessOverTime(water));
        StartCoroutine(ReduceFullnessOverTime(food));
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
        currHealth = currHealth + newConsumable.health;
       healthZero = IsAddOnZero((float)newConsumable.health);
         foodZero = IsAddOnZero((float)newConsumable.food);
         waterZero = IsAddOnZero((float)newConsumable.water);

        //health.UpdateCurrentValue((float)newConsumable.health);
     //   health.AddOn();
        StartCoroutine(IncreaseFullnessOverTime(health,(float)newConsumable.health));
        StartCoroutine(IncreaseFullnessOverTime(water, (float)newConsumable.water));
        // water.UpdateCurrentValue((float)newConsumable.water);
        StartCoroutine(IncreaseFullnessOverTime(food, (float)newConsumable.food));
       // food.UpdateCurrentValue((float)newConsumable.food);
        if(consumeItem !=null)
            consumeItem.Invoke(true);
    }
    public bool IsAddOnZero(float addOn)
    {
        if(addOn !=0 )
            return false;
        return true;
    }
    public void TakeDamage(float damageAmount)
    {
        if (health.GetCurrentValue() > 0)
        {
            //health.UpdateCurrentValue(-damageAmount);
            StartCoroutine(ReduceFullness(health, damageAmount));
        }
        else
        {
            //Die();
        }
    }

  /*  private void Update()
    {
       
        currentTimeCountdown -= Time.deltaTime;

        if (currentTimeCountdown < 0f)
        {
           
            water.RemoveValue(3);
            food.RemoveValue(1);
            currentTimeCountdown = timeCountdown;
            if (consumeItem != null)
                consumeItem.Invoke();
        }
    }*/
    private void OnCloseToDying(Stats stat)
    {
        
        string msg = stat.GetName() + " imminent";
        PopUpMessagesManager.instance.ShowPopUpMessage(msg);
        StartCoroutine(ReduceFullnessOverTime(health));
    }
    private IEnumerator ReduceFullnessOverTime(Stats stat)
    {
        float maxValue = stat.GetMaxValue();
        float rate = stat.GetReducingRate();
        // This will keep reducing fullness until it reaches 0
        while (stat.GetCurrentValue() > 30f)
        {
           
            stat.UpdateCurrentValue(-rate * Time.deltaTime);      // Ensure it doesn't go below 0
            stat.SetCurrentVisualValue(stat.GetCurrentValue());
            if (consumeItem != null)
                consumeItem.Invoke(false);
            yield return null;  // Wait for the next frame before continuing
        }
        if (stat.GetName() != "health")
            OnCloseToDying(stat);
        while (stat.GetCurrentValue() > 0f)
        {

            stat.UpdateCurrentValue(-rate * Time.deltaTime);// * Time.deltaTime);      // Ensure it doesn't go below 0
            stat.SetCurrentVisualValue(stat.GetCurrentValue());
            if (consumeItem != null)
                consumeItem.Invoke(false);
            yield return null;  // Wait for the next frame before continuing
        }
        // Trigger hunger state or any other consequence

    }
    private IEnumerator IncreaseFullnessOverTime(Stats stat, float addOn)
    {
        float currentVal = stat.GetCurrentValue();
        
        float targetVal = currentVal + addOn;
        stat.UpdateCurrentValue(addOn);
        float rate = 3f;
        float step = 0f;
        // This will keep reducing fullness until it reaches 0
        while (stat.GetCurrentVisualValue() < targetVal)
        {
            
            
            stat.SetCurrentVisualValue(Mathf.Lerp(currentVal,targetVal, step));      
            step += rate * Time.deltaTime; 
            if (consumeItem != null)
                consumeItem.Invoke(false);
            yield return null;  // Wait for the next frame before continuing
        }
        
        // Trigger hunger state or any other consequence

    }
    private IEnumerator ReduceFullness(Stats stat, float addOn)
    {
        float currentVal = stat.GetCurrentValue();

        float targetVal = currentVal - addOn;
        stat.UpdateCurrentValue(-addOn);
        float rate = 3f;
        float step = 0f;
        // This will keep reducing fullness until it reaches 0
        while (stat.GetCurrentVisualValue() > targetVal)
        {


            stat.SetCurrentVisualValue(Mathf.Lerp(currentVal, targetVal, step));
            step -= rate * Time.deltaTime;
            if (consumeItem != null)
                consumeItem.Invoke(false);
            yield return null;  // Wait for the next frame before continuing
        }

    }
}
