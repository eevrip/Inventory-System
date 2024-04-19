using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The object that is Collectable, can be picked up by the player
public class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemObject item;
   
    public ItemObject Item {  get { return item; } set {item = value; } }
    
    public void Interact() {
      

        PickUp();
    }

    public void PickUp() {


        bool isPickedUp = PlayerInventory.instance.Add(Item);

        if (isPickedUp)
        {
          
            Debug.Log("Picked Up Item");
            Destroy(gameObject);
        }
        
    }
}
