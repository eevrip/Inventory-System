using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
   [SerializeField] private int health;
    //GameObject breakableItem = transform.gameObject;

    private bool isBroken = false;
    private SpawningItem spawn;

    [SerializeField] private ItemObject item;
    public ItemObject Item { get { return item; } set { item = value; } }
    public void Start()
    {
        spawn = GameObject.FindObjectOfType<SpawningItem>();

    }
   
    public void Interact()
    {
       
       
        BreakItem(PlayerStateManager.instance.damage.GetValue());
    }

    void BreakItem(int damage)
    {


        health = health - damage; //Damage of equipment
        Debug.Log("Remaining strength " + health);


        if (health <= 0f)
            isBroken = true;

        if (isBroken)
        {

            Debug.Log("Item is broken");

            spawn.SpawnItem(item.prefab, transform.position, Quaternion.identity, transform);
            Destroy(transform.GetChild(0).gameObject);
        }

    }


}
