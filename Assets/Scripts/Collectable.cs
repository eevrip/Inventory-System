using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The object that is Collectable, can be picked up by the player
public class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemObject item;
   
    public ItemObject Item {  get { return item; } set {item = value; } }
    [SerializeField]
    private PlayerAnimation anim;

    public void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAnimation>();
        
    }
    public void Interact() {
      

        PickUp();
       
    }

    public void PickUp() {
        if (anim != null)
        {

            anim.SetIsPickingUp();
        }
        StartCoroutine(ExecuteAfterTime(0.1f));
        bool isPickedUp = PlayerInventory.instance.Add(Item);

        if (isPickedUp)
        {
          
            Debug.Log("Picked Up Item");
            
              // if( anim.SetIsPickingUp())
                    Destroy(gameObject,0.1f);
            }
        
        
    }

private IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    }   
}
