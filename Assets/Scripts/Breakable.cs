using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
   [SerializeField] private int health;
    //GameObject breakableItem = transform.gameObject;

    private bool isBroken = false;
    private SpawningItem spawn;
    [SerializeField]
    private PlayerAnimation anim;
    [SerializeField] private ItemObject item;
    public ItemObject Item { get { return item; } set { item = value; } }
    public void Start()
    {
        spawn = GameObject.FindObjectOfType<SpawningItem>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAnimation>();
    }
   
    public void Interact()
    {
       anim.SetIsAttacking();
       StartCoroutine(ExecuteAfter(0.4f));
       
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

    private IEnumerator ExecuteAfter(float time)
    {
       
        yield return new WaitForSeconds(time);
        BreakItem(PlayerStateManager.instance.damage.GetValue());
    }
}


