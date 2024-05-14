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
    
    int currHealth;
    public Stats health;
    public Stats water;
    public Stats food;
    public Stats damage;
    public Stats defence;
    ToolManager toolManager;
    EquipmentManager equipmentManager;
    // Stats defense;
    void Start()
    {
        toolManager = ToolManager.instance;
        toolManager.onToolUpdateCallback += onToolUpdate;
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentUpdateCallback += onEquipmentUpdate;
    }

    public void onToolUpdate(ToolObject item)
    {
        // damage.SetValue(item.damage); //replace the damage caused
        // defense = item.defense
        damage.AddValue(toolManager.currToolStats[0]);
       // defence.SetValue(toolManager.currToolStats[0]);


    }
    public void onEquipmentUpdate(EquipmentObject item)
    {
        // damage.SetValue(item.damage); //replace the damage caused
        // defense = item.defense
       // damage.SetValue(toolManager.currToolStats[0]);
        defence.AddValue(equipmentManager.currEquipStats[0]);


    }
    public void onConsumableUpdate(ConsumableObject newConsumable)
    {
        currHealth = currHealth + newConsumable.health;
        health.AddValue(newConsumable.health);
        water.AddValue(newConsumable.water);
        food.AddValue(newConsumable.food);
    }

    public void TakeDamage(int damageAmount)
    {
        if (health.GetCurrentValue() > 0)
        {
            health.RemoveValue(damageAmount);
        }
        else
        {
            //Die();
        }
    }
}
