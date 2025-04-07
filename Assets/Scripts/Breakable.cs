using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Breakable : MonoBehaviour, IInteractable
{
    [SerializeField] private float health;
    //GameObject breakableItem = transform.gameObject;
    [SerializeField] private string nameItem;
    private bool isBroken = false;
    private SpawningItem spawn;
    [SerializeField]
    private PlayerAnimation anim;
    [SerializeField] private ItemObject item;

    [SerializeField] private int amount = 1;
    public ItemObject Item { get { return item; } set { item = value; } }
    [SerializeField] private string message;
    public string Message
    {
        get { return message; }
        set
        {
            message = value;
        }
    }
    public void Start()
    {
        message = message +" "+ nameItem;
        spawn = GameObject.FindObjectOfType<SpawningItem>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAnimation>();
    }

    public void Interact()
    {
        anim.SetIsAttacking();
        StartCoroutine(ExecuteAfter(0.4f));

    }

    void BreakItem(float damage)
    {


        health = health - damage; //Damage of equipment
        Debug.Log("Remaining strength " + health + "dam " + damage);


        if (health <= 0f)
            isBroken = true;

        if (isBroken)
        {

            Debug.Log("Item is broken");
            if (amount > 0)
            {
                Vector3 offSet = new Vector3(0f, 0.5f, 0f);
                float degreesOffset = 2 * Mathf.PI / (float)amount;

                for (int i = 0; i < amount; i++)
                {
                    spawn.SpawnItem(item.prefab, transform.position + offSet, Quaternion.identity, transform);
                    Vector3 xzOffset = new Vector3(Mathf.Sin(degreesOffset * i), 0f, Mathf.Cos(degreesOffset * i));

                    offSet = offSet + xzOffset;
                }


            }

            Destroy(transform.GetChild(0).gameObject);
        }

    }

    private IEnumerator ExecuteAfter(float time)
    {

        yield return new WaitForSeconds(time);
        BreakItem(PlayerStateManager.instance.damage.GetValue());
    }

}


