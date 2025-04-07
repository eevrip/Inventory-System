using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private IInteractable interactableItem;
    [SerializeField] private float distanceCheck = 10f; //the distance of the ray
    [SerializeField] private LayerMask layerMask;

    private UIManager uiManager;

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
            interactableItem = hit.collider.transform.parent.gameObject.GetComponent<IInteractable>();



            if (interactableItem != null)
            {
                uiManager.SetInformationalText(interactableItem.Message);
                if (Input.GetMouseButtonDown(0))
                { //Can interact with it
                    interactableItem.Interact();
                }
            }
            else
            {
                uiManager.HideInformationalText();
            }


        }
        else {
            uiManager.HideInformationalText();
        }


       
    }


}
