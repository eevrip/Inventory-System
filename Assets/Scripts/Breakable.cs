using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
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
    [SerializeField] private bool isInteractable = true;
    public string Message
    {
        get { return message; }
        set
        {
            message = value;
        }
    }
    public bool IsInteractable
    {
        get { return isInteractable; }
        set
        {
            isInteractable = value;
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
    private float baseRadius = 0.5f;
    private float radiusJitter = 0.2f;
 
    private float burstForce = 3f;
    void BreakItem(float damage)
    {


        health = health - damage; //Damage of equipment
        Debug.Log("Remaining strength " + health + "dam " + damage);


        if (health <= 0f)
            isBroken = true;

        if (isBroken)
        {
            //isBroken = false;

            Debug.Log("Item is broken");
            if (amount > 0)
            {
                isBroken = false;
              
               
                for (int i = 0; i < amount; i++)
                { 
                  

                    float angle = i * 2 * Mathf.PI / (float)amount;

                    // Add jitter to angle and radius
                    
                    float angleOffset = Random.Range(-angle/2f,angle/2f );
                    float radiusOffset = Random.Range(-radiusJitter, radiusJitter);
                    float finalAngle = angle + angleOffset;
                    float finalRadius = baseRadius + radiusOffset;

                    // Calculate spawn position
                    Vector3 offset = new Vector3(Mathf.Cos(finalAngle), 0f, Mathf.Sin(finalAngle)) * finalRadius;
                    Vector3 spawnPos = transform.position + offset;

                    // Random rotation
                    Quaternion randomRot = Quaternion.Euler(Random.Range(-90f, 90f), Random.Range(0f, 360f), Random.Range(-90f, 90f));

                    // Instantiate loot
                  
                    GameObject loot = spawn.SpawnItem(item.prefab, spawnPos, randomRot, transform);
                    // Add burst force if it has Rigidbody
                      Rigidbody rb = loot.GetComponent<Rigidbody>();
                    Collider col = loot.GetComponentInChildren<Collider>();
                    
                      if (rb != null)
                      {
                          Vector3 burstDir = (loot.transform.position - transform.position +new Vector3(0f, 1f, 0f)).normalized;
                          rb.AddForce(burstDir * burstForce, ForceMode.Impulse);
                      }
                   StartCoroutine(EnableColliderAfterDelay(col));
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
private IEnumerator EnableColliderAfterDelay(Collider col)
{
        col.enabled = false;
      
    yield return new WaitForSeconds(0.3f);
        col.enabled = true;
       

    }

}


