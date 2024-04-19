using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
  IInteractable interactableItem;
    public float distanceCheck = 5f; //the distance of the ray
    public LayerMask layerMask;

    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //Camera is the player
        uiManager = UIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!uiManager.IsUIEnabled)
            Interacting();
       
    }

    void Interacting()
    {
        //Interact with item-collect, attack, break...
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //A ray starting from the camera to the mouse position

        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 5, Color.yellow);
        if (Physics.Raycast(ray, out hit, distanceCheck, layerMask))
        { //If the ray hits something, in range distanceCheck
            interactableItem = hit.collider.transform.parent.gameObject.GetComponent<Collectable>();


            if (interactableItem == null)
                interactableItem = hit.collider.transform.parent.gameObject.GetComponent<Breakable>();

            if (interactableItem == null)
                interactableItem = hit.collider.transform.parent.gameObject.GetComponent<Storage>();

            if (interactableItem != null)
            {

                if (Input.GetMouseButtonDown(0))
                { //Can interact with it
                    interactableItem.Interact();
                }
            }


        }


        //Place item that you hold
        if (Input.GetMouseButtonDown(1))
        { //Push the right mouse button down

        }
    }


}
