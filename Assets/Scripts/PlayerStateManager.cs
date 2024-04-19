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
    
    float currHealth;
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
        damage.SetValue(toolManager.currToolStats[0]);
       // defence.SetValue(toolManager.currToolStats[0]);


    }
    public void onEquipmentUpdate(EquipmentObject item)
    {
        // damage.SetValue(item.damage); //replace the damage caused
        // defense = item.defense
       // damage.SetValue(toolManager.currToolStats[0]);
        defence.SetValue(equipmentManager.currEquipStats[0]);


    }
    public void UpdateState(ConsumableObject newConsumable)
    {
        currHealth = currHealth + newConsumable.health;
    }


}
